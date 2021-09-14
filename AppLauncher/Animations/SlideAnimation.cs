using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AppLauncher.Animations
{
    public class SlideAnimation
    {
        #region Public Methods

        public static async Task Play(FrameworkElement element, double left)
        {
            Storyboard storyboard = new Storyboard();

            BounceEase bounceEase = new BounceEase
            {
                Bounces = 1,
                EasingMode = EasingMode.EaseOut,
                Bounciness = 30
            };

            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.25),
                To = new Thickness(left, 0, 0, 0),
                DecelerationRatio = 0.9f,
                EasingFunction = bounceEase
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
            storyboard.Begin(element);

            await Task.Delay(TimeSpan.FromSeconds(0.25));
        }

        #endregion
    }
}
