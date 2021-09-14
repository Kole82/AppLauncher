using AppLauncher.Animations;
using AppLauncher.Data.Interfaces;
using AppLauncher.Models.Xml;
using AppLauncher.MvvmFramework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AppLauncher.ViewModels
{
    public class CarouselViewModel : BaseViewModel, IDisposable
    {
        #region Private Fields

        private int _selectedIndex;
        private PageViewModel _selectedPage;
        private readonly IRepository _repo;
        private bool _disposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        //public CarouselViewModel(IRepository repo)
        public CarouselViewModel()
        {
            //_repo = repo;
            _repo = IoC.Get<IRepository>();

            InitializePages();

            _selectedIndex = 0;
            _selectedPage = Pages[0];

            _repo.PageAdded += new EventHandler<PageEventArgs>(async (sender, args) =>
            {
                PageViewModel pageViewModel = new PageViewModel(args.Page);
                PageIndicatorViewModel pageIndicatorViewModel = new PageIndicatorViewModel();

                if (_selectedIndex + 1 <= Pages.Count - 1)
                {
                    Pages.Insert(_selectedIndex + 1, pageViewModel);
                    PageIndicators.Insert(_selectedIndex + 1, pageIndicatorViewModel);
                }
                else
                {
                    Pages.Add(pageViewModel);
                    PageIndicators.Add(pageIndicatorViewModel);
                }

                await AnimationControl.SwipeForward();
            });

            _repo.PageUpdated += new EventHandler<PageEventArgs>((sender, args) =>
            {
                Pages[_selectedIndex] = new PageViewModel(args.Page);
            });
        }

        #endregion

        #region Public Properties

        public ObservableCollection<PageViewModel> Pages { get; private set; }
        public ObservableCollection<PageIndicatorViewModel> PageIndicators { get; private set; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                PageIndicators[_selectedIndex].IsActive = false;

                _selectedIndex = value;
                SelectedPage = Pages[_selectedIndex];

                PageIndicators[_selectedIndex].IsActive = true;

                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public PageViewModel SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                OnPropertyChanged(nameof(SelectedPage));
            }
        }

        #endregion

        #region Commands

        public ICommand AddShortcutCommand => new RelayCommand(file =>
        {
            Shortcut shortcut = new Shortcut
            {
                Id = Guid.NewGuid(),
                Name = ExtractShortcutData.GetName(file.ToString()),
                Path = ExtractShortcutData.GetFullPath(file.ToString()),
                Icon = ExtractShortcutData.GetIcon(file.ToString())
            };

            SelectedPage.Page.Shortcuts.Add(shortcut);
            UpdatePageCommand.Execute(SelectedPage.Page);
        });

        //TODO: introduce an enum to allow adding pages to different positions
        public ICommand AddPageCommand => new RelayCommand(position =>
        {
            _repo.Add(_selectedIndex);
        });

        public ICommand UpdatePageCommand => new RelayCommand(page =>
        {
            _repo.Update((Page)page);
        });

        #endregion

        #region Private Helpers

        private void InitializePages()
        {
            Pages = new ObservableCollection<PageViewModel>(_repo.GetAll().Select(page => new PageViewModel(page)));
            PageIndicators = new ObservableCollection<PageIndicatorViewModel>(Pages.Select(page => new PageIndicatorViewModel()));
            PageIndicators[_selectedIndex].IsActive = true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            // Release any managed resources here.
            if (disposing)
            {
                //TODO: unsubscribe from events
                //TODO: clear colletions
            }

            // Dispose of any unmanaged resources not wrapped in safe handles.

            _disposed = true;
        }

        #endregion
    }
}
