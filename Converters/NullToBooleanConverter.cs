using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFBoilerplate.Converters
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if parameter is "Inverse" to reverse the logic
            bool inverse = parameter?.ToString()?.ToLower() == "inverse";
            
            if (inverse)
            {
                return value == null;
            }
            else
            {
                return value != null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}