using AppLauncher.MvvmFramework;
using Microsoft.Win32;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppLauncher.ViewModels
{
    public class MainWindowViewModel
    {
        #region Commands

        public ICommand ShortcutDialogCommand => new RelayCommand(async obj =>
        {
            await Task.Run(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Add File";

                if (openFileDialog.ShowDialog() == true)
                {
                    App.Current.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                    {
                        IoC.Get<CarouselViewModel>().AddShortcutCommand.Execute(openFileDialog.FileName);
                    });
                }
            });
        });

        public ICommand FolderDialogCommand => new RelayCommand(obj =>
        {
            using (System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    IoC.Get<CarouselViewModel>().AddShortcutCommand.Execute(folderBrowserDialog.SelectedPath);
                }
            }
        });

        public ICommand AddPageCommand => new RelayCommand(obj =>
        {
            IoC.Get<CarouselViewModel>().AddPageCommand.Execute(obj);
        });

        #endregion
    }
}
