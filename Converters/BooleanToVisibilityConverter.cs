using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFBoilerplate.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Check if parameter is "Inverse" to reverse the logic
                bool inverse = parameter?.ToString()?.ToLower() == "inverse";
                
                if (inverse)
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }
                else
                {
                    return boolValue ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool inverse = parameter?.ToString()?.ToLower() == "inverse";
                
                if (inverse)
                {
                    return visibility == Visibility.Collapsed;
                }
                else
                {
                    return visibility == Visibility.Visible;
                }
            }

            return false;
        }
    }
}