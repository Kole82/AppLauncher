using AppLauncher.MvvmFramework;

namespace AppLauncher.ViewModels
{
    public class PageIndicatorViewModel : BaseViewModel
    {
        #region Private Fields

        private bool isActive;

        #endregion

        #region Public Properties

        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        #endregion
    }
}
