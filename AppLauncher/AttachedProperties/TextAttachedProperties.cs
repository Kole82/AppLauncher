using System.Windows;
using System.Windows.Controls;

namespace AppLauncher.AttachedProperties
{
    public class TextAttachedProperties
    {
        #region Attached Properties

        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool),
            typeof(TextAttachedProperties), new FrameworkPropertyMetadata(false, OnValueChanged));

        public static void SetIsFocused(UIElement element, bool value)
        {
            element.SetValue(IsFocusedProperty, value);
        }

        public static bool GetIsFocused(UIElement element)
        {
            return (bool)element.GetValue(IsFocusedProperty);
        }

        public static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.Focus();
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }

        #endregion
    }
}
