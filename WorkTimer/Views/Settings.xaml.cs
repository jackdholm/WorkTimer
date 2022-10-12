using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private static readonly Regex _regex = new Regex("[0-9]");
        private static readonly Regex _regexDouble = new Regex("[0-9.]");

        public Settings()
        {
            InitializeComponent();
            _defaultBackground = new SolidColorBrush(Colors.White);
            _focusedBackground = new SolidColorBrush(Colors.DarkGoldenrod);
            _defaultForeground = new SolidColorBrush(Colors.Black);
            _focusedForeground = new SolidColorBrush(Colors.White);
            HoursInput.CaretBrush = MinutesInput.CaretBrush = _focusedBackground;
            BreakHoursInput.CaretBrush = BreakMinutesInput.CaretBrush = _focusedBackground;
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

        private void BreakHoursInput_SelectionChanged(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void BreakHoursInput_LostFocus(object sender, RoutedEventArgs e)
        {
            BreakHoursInput.Background = _defaultBackground;
            BreakHoursInput.Foreground = _defaultForeground;
        }

        private void BreakHoursInput_GotFocus(object sender, RoutedEventArgs e)
        {
            BreakHoursInput.CaretIndex = 2;
            BreakHoursInput.Background = _focusedBackground;
            BreakHoursInput.Foreground = _focusedForeground;
        }

        private void BreakHoursInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            BreakHoursInput.Text = BreakHoursInput.Text[1].ToString() + e.Text;
            BreakHoursInput.CaretIndex = 2;
        }

        private void BreakMinutesInput_LostFocus(object sender, RoutedEventArgs e)
        {
            BreakMinutesInput.Background = _defaultBackground;
            BreakMinutesInput.Foreground = _defaultForeground;
        }

        private void BreakMinutesInput_GotFocus(object sender, RoutedEventArgs e)
        {
            BreakMinutesInput.CaretIndex = 2;
            BreakMinutesInput.Background = _focusedBackground;
            BreakMinutesInput.Foreground = _focusedForeground;
        }

        private void BreakMinutesInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            string last = BreakMinutesInput.Text[1].ToString();
            if (Convert.ToInt32(last) < 6)
                BreakMinutesInput.Text = last + e.Text;
            else
                BreakMinutesInput.Text = "0" + e.Text;
            BreakMinutesInput.CaretIndex = 2;
        }

        private void PauseLimitInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_regex.IsMatch(e.Text))
            {
                e.Handled = true;
                return;
            }
            PauseLimitInput.Text = e.Text;
        }

        private void BreakRatioInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double ratio;
            try
            {
                ratio = Convert.ToDouble(BreakRatioInput.Text);
            }
            catch
            {
                ratio = 0;
            }
            if (!_regexDouble.IsMatch(BreakRatioInput.Text + e.Text) || (ratio >= 1 && e.Text != "0") || (e.Text == "." && BreakRatioInput.Text.Length > 1))
            {
                e.Handled = true;
                return;
            }
            //if (!_regexDouble.IsMatch(e.Text) || (ratio >= 1 && e.Text != "0") || (e.Text == "." && BreakRatioInput.Text.Length > 1))
            //{
            //    e.Handled = true;
            //    return;
            //}
            //else if (e.Text == "0")
            //{
            //    e.Handled = true;
            //    BreakRatioInput.Text = "0.";
            //    BreakRatioInput.CaretIndex = 2;
            //    return;
            //}
            if (BreakRatioInput.Text.Length == 0)
            {
                if (e.Text == ".")
                {
                    BreakRatioInput.Text = "0.";
                    e.Handled = true;
                }
                else if (e.Text != "0" && e.Text != "1")
                {
                    e.Handled = true;
                }
            }
            if (BreakRatioInput.Text.Length == 1 && e.Text != ".")
            {
                BreakRatioInput.Text = ratio == 0 ? "1" : "0";
                e.Handled = true;
            }
            BreakRatioInput.CaretIndex = BreakRatioInput.Text.Length;
        }
    }
}
