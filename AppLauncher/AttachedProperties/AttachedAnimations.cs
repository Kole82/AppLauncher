using System.Windows;

namespace AppLauncher.AttachedProperties
{
    public class AttachedAnimations
    {
        #region Attached Properties

        public static readonly DependencyProperty FadeInOutProperty = DependencyProperty.RegisterAttached("FadeInOut", typeof(bool),
            typeof(AttachedAnimations), new FrameworkPropertyMetadata(false, DoAnimation));

        public static void SetFadeInOut(UIElement element, bool value)
        {
            element.SetValue(FadeInOutProperty, value);
        }

        public static bool GetFadeInOut(UIElement element)
        {
            return (bool)element.GetValue(FadeInOutProperty);
        }

        public static async void DoAnimation(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                await Animations.FadeAnimation.Play(o as FrameworkElement, true);
            }
            else
            {
                await Animations.FadeAnimation.Play(o as FrameworkElement, false);
            }
        }

        #endregion
    }
}
