using AppLauncher.MvvmFramework;
using AppLauncher.ViewModels;
using System.Windows.Controls;

namespace AppLauncher.Controls
{
    /// <summary>
    /// Interaction logic for Carousel.xaml
    /// </summary>
    public partial class Carousel : UserControl
    {
        #region Constructors

        public Carousel()
        {
            InitializeComponent();

            DataContext = IoC.Get<CarouselViewModel>();
        }

        #endregion
    }
}
