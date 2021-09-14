using AppLauncher.MvvmFramework;
using System.Windows;

namespace AppLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //TODO: single instance

        private AppActivation appActivation = new AppActivation();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.Setup();
            appActivation.SetHooks();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            appActivation.RemoveHooks();
        }
    }
}
