using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorkTimer.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private SolidColorBrush _defaultBackground;
        private SolidColorBrush _focusedBackground;
        private SolidColorBrush _defaultForeground;
        private SolidColorBrush _focusedForeground;

        public Settings()
        {
            InitializeComponent();
            _defaultBackground = new SolidColorBrush(Colors.White);
            _focusedBackground = new SolidColorBrush(Colors.DarkGoldenrod);
            _defaultForeground = new SolidColorBrush(Colors.Black);
            _focusedForeground = new SolidColorBrush(Colors.White);
            HoursInput.CaretBrush = MinutesInput.CaretBrush = _focusedBackground;
        }

        private void HoursInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int num;
            try
            {
                num = Convert.ToInt32(e.Text);
            }
            catch
            {
                e.Handled = true;
            }
            HoursInput.Text = HoursInput.Text[1].ToString() + e.Text;
            HoursInput.CaretIndex = 2;
        }

        private void HoursInput_GotFocus(object sender, RoutedEventArgs e)
        {
            HoursInput.CaretIndex = 2;
            HoursInput.Background = _focusedBackground;
            HoursInput.Foreground = _focusedForeground;
        }

        private void HoursInput_LostFocus(object sender, RoutedEventArgs e)
        {
            HoursInput.Background = _defaultBackground;
            HoursInput.Foreground = _defaultForeground;
        }

        private void HoursInput_SelectionChanged(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void MinutesInput_GotFocus(object sender, RoutedEventArgs e)
        {
            MinutesInput.CaretIndex = 2;
            MinutesInput.Background = _focusedBackground;
            MinutesInput.Foreground = _focusedForeground;
        }

        private void MinutesInput_LostFocus(object sender, RoutedEventArgs e)
        {
            MinutesInput.Background = _defaultBackground;
            MinutesInput.Foreground = _defaultForeground;
        }

        private void MinutesInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int num;
            try
            {
                num = Convert.ToInt32(e.Text);
            }
            catch
            {
                e.Handled = true;
                return;
            }
            string last = MinutesInput.Text[1].ToString();
            if (Convert.ToInt32(last) < 6)
                MinutesInput.Text = last + e.Text;
            else
                MinutesInput.Text = "0" + e.Text;
            MinutesInput.CaretIndex = 2;
        }

    }
}
