using AppLauncher.Models.Interfaces;
using AppLauncher.MvvmFramework;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AppLauncher.ViewModels
{
    public class ShortcutViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly IShortcut _shortcut;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShortcutViewModel(IShortcut shortcut)
        {
            _shortcut = shortcut;
        }

        #endregion

        #region Public Wrapper Properties

        public Guid Id => _shortcut.Id;

        public string Name
        {
            get => _shortcut.Name;
            set
            {
                _shortcut.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Path
        {
            get => _shortcut.Path;
            set
            {
                _shortcut.Path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public byte[] Icon
        {
            get => _shortcut.Icon;
            set
            {
                _shortcut.Icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        #endregion

        #region Commands

        public ICommand LaunchCommand => new RelayCommand(obj =>
        {
            //TODO: use a platform independent UI manager
            Process p = Process.Start(Path);
            App.Current.MainWindow.Hide();
        });

        #endregion
    }
}
