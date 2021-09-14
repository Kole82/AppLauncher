using AppLauncher.Animations;
using AppLauncher.MvvmFramework;
using AppLauncher.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppLauncher.Controls
{
    /// <summary>
    /// Interaction logic for SlidingPanel.xaml
    /// </summary>
    public partial class SlidingPanel : UserControl
    {
        #region Private Fields

        /// <summary>
        /// The minimum distance for a swipe to cover to be valid.
        /// </summary>
        private const double REQUIRED_SWIPE_DISTANCE = 100;

        private bool isLeftMouseDown = false;
        private Point oldMousePosition;
        private double delta;
        private double deltaAggregate = 0d;
        private bool isAnimating = false;

        #endregion

        #region Constructors

        public SlidingPanel()
        {
            InitializeComponent();

            AnimationControl.Element = this;
        }

        #endregion

        #region Mouse Event Handlers

        private void Carousel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isAnimating)
            {
                return;
            }

            isLeftMouseDown = true;
            oldMousePosition = e.GetPosition(this);
        }

        private void Carousel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLeftMouseDown)
            {
                delta = e.GetPosition(this).X - oldMousePosition.X;
                deltaAggregate += delta;

                if (IoC.Get<CarouselViewModel>().SelectedIndex == 0 && deltaAggregate > 0)
                {
                    Margin = new Thickness(delta * 0.15f, 0, 0, 0);
                }
                else if (IoC.Get<CarouselViewModel>().SelectedIndex == IoC.Get<CarouselViewModel>().Pages.Count - 1 && deltaAggregate < 0)
                {
                    Margin = new Thickness(AnimationControl.targetLeftMargin + (delta * 0.15f), 0, 0, 0);
                }
                else
                {
                    Margin = new Thickness(Margin.Left + delta, 0, 0, 0);
                }
            }
        }

        private async void Carousel_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isLeftMouseDown = false;

            if (!IsValidSwipe)
            {
                isAnimating = true;

                await AnimationControl.SwipeBackOnInvalid();

                isAnimating = false;
            }
            else
            {
                // swiping backward (from left to right)
                if (deltaAggregate > 0)
                {
                    isAnimating = true;

                    await AnimationControl.SwipeBackward();

                    isAnimating = false;
                }
                // swiping forward (from right to left)
                else if (deltaAggregate < 0)
                {
                    isAnimating = true;

                    await AnimationControl.SwipeForward();

                    isAnimating = false;
                }
            }

            deltaAggregate = 0;
        }

        private async void Carousel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                await AnimationControl.SwipeForward();
            }
            else if (e.Delta < 0)
            {
                await AnimationControl.SwipeBackward();
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// A swipe is considered valid if it travels the minimum required distance as set by <see cref="REQUIRED_SWIPE_DISTANCE"/>.
        /// </summary>
        private bool IsValidSwipe => Math.Abs(deltaAggregate) >= REQUIRED_SWIPE_DISTANCE;

        #endregion
    }
}
