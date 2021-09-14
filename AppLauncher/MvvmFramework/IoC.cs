using AppLauncher.Data.Interfaces;
using AppLauncher.Data.Xml;
using AppLauncher.ViewModels;
using Ninject;

namespace AppLauncher.MvvmFramework
{
    public static class IoC
    {
        #region Public Properties

        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static T Get<T>() => Kernel.Get<T>();

        #endregion Public Properties

        #region IoC Setup

        public static void Setup() => BindViewModels();

        private static void BindViewModels()
        {
            //Kernel.Bind<IRepository>().ToConstant(new Repository("data.xml"));
            Kernel.Bind<IRepository>().To<PageRepository>().InSingletonScope();

            Kernel.Bind<MainWindowViewModel>().ToConstant(new MainWindowViewModel());
            //Kernel.Bind<CarouselViewModel>().ToConstant(new CarouselViewModel(Kernel.Get<IRepository>()));
            Kernel.Bind<CarouselViewModel>().To<CarouselViewModel>().InSingletonScope();

            Kernel.Bind<PageViewModel>().To<PageViewModel>();
        }

        #endregion IoC Setup
    }
}
