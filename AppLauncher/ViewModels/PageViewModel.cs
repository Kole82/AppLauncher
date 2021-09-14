using AppLauncher.Models.Interfaces;
using AppLauncher.MvvmFramework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AppLauncher.ViewModels
{
    public class PageViewModel : BaseViewModel
    {
        #region Private Fields

        //private readonly IPage _page;
        private IPage _page;
        private bool _isNameEditing;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageViewModel(IPage page)
        {
            _page = page;

            InitializeShortcuts();

            Title = new EditableTextBlockViewModel
            {
                OldText = page.Name,
                Page = _page
            };
        }

        #endregion

        #region Public Properties

        public EditableTextBlockViewModel Title { get; set; }

        public ObservableCollection<ShortcutViewModel> Shortcuts { get; private set; }

        public IPage Page => _page;

        #endregion

        #region Public State Properties

        public double Width => SystemParameters.PrimaryScreenWidth;

        public bool IsNameEditing
        {
            get => _isNameEditing;
            set
            {
                _isNameEditing = value;
                OnPropertyChanged(nameof(IsNameEditing));
            }
        }

        #endregion

        #region Commands

        public ICommand EditPageNameCommand => new RelayCommand(path =>
        {
            IsNameEditing = true;
        });

        #endregion

        #region Private Helpers

        private void InitializeShortcuts()
        {
            if (_page.Shortcuts != null)
            {
                Shortcuts =
                    new ObservableCollection<ShortcutViewModel>(_page.Shortcuts.Select(shortcut => new ShortcutViewModel(shortcut)));
            }
        }

        #endregion
    }
}
