using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AppLauncher.Animations
{
    public class FadeAnimation
    {
        #region Public Methods

        public static async Task Play(FrameworkElement element, bool flag)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                To = flag? 1 : 0
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);
            storyboard.Begin(element);

            await Task.Delay(TimeSpan.FromSeconds(0.3));
        }

        #endregion
    }
}
