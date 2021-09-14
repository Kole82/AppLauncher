using AppLauncher.Models.Interfaces;
using AppLauncher.Models.Xml;
using AppLauncher.MvvmFramework;
using System.Windows.Input;

namespace AppLauncher.ViewModels
{
    public class EditableTextBlockViewModel : BaseViewModel
    {
        #region Private Fields

        private string oldText;
        private string newText;
        private bool isEditing;

        #endregion

        #region Public Properties

        public string OldText
        {
            get => oldText;
            set
            {
                oldText = value;
                OnPropertyChanged(nameof(OldText));
            }
        }

        public string NewText
        {
            get => newText;
            set
            {
                newText = value;
                OnPropertyChanged(nameof(NewText));
            }
        }

        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        public IPage Page { get; set; }

        #endregion

        #region Commands

        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EditableTextBlockViewModel()
        {
            EditCommand = new RelayCommand(obj => Edit());
            CancelCommand = new RelayCommand(obj => Cancel());
            SaveCommand = new RelayCommand(obj => Save());
        }

        #endregion

        #region Command Methods

        private void Edit()
        {
            NewText = OldText;
            IsEditing = true;
        }

        private void Cancel()
        {
            IsEditing = false;
            NewText = OldText;
        }

        private void Save()
        {
            OldText = NewText;
            Page.Name = OldText;

            IsEditing = false;

            IoC.Get<CarouselViewModel>().UpdatePageCommand.Execute((Page)Page);
        }

        #endregion
    }
}
