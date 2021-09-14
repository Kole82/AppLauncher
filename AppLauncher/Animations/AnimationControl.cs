using AppLauncher.MvvmFramework;
using AppLauncher.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AppLauncher.Animations
{
    public class AnimationControl
    {
        #region Private Fields

        public static double targetLeftMargin = 0d;
        private static double screenWidth = SystemParameters.WorkArea.Width;

        #endregion

        #region Public Properties

        public static FrameworkElement Element { get; set; }

        #endregion

        //TODO: needs refactoring
        #region Public Methods

        public static async Task SwipeForward()
        {
            if (IoC.Get<CarouselViewModel>().SelectedIndex < IoC.Get<CarouselViewModel>().Pages.Count - 1)
            {
                IoC.Get<CarouselViewModel>().SelectedIndex++;
                targetLeftMargin -= screenWidth;
            }

            await SlideAnimation.Play(Element, targetLeftMargin);

            Element.BeginAnimation(FrameworkElement.MarginProperty, null);
            Element.SetValue(FrameworkElement.MarginProperty, new Thickness(targetLeftMargin, 0, 0, 0));
        }

        public static async Task SwipeBackward()
        {
            if (IoC.Get<CarouselViewModel>().SelectedIndex > 0)
            {
                IoC.Get<CarouselViewModel>().SelectedIndex--;
                targetLeftMargin += screenWidth;
            }

            await SlideAnimation.Play(Element, targetLeftMargin);

            Element.BeginAnimation(FrameworkElement.MarginProperty, null);
            Element.SetValue(FrameworkElement.MarginProperty, new Thickness(targetLeftMargin, 0, 0, 0));
        }

        public static async Task SwipeBackOnInvalid()
        {
            await SlideAnimation.Play(Element, targetLeftMargin);

            Element.BeginAnimation(FrameworkElement.MarginProperty, null);
            Element.SetValue(FrameworkElement.MarginProperty, new Thickness(targetLeftMargin, 0, 0, 0));
        }

        #endregion
    }
}
