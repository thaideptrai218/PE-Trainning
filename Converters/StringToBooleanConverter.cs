using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFBoilerplate.Converters
{
    public class StringToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && parameter is string targetValue)
            {
                return string.Equals(stringValue, targetValue, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue && parameter is string targetValue)
            {
                return targetValue;
            }

            return null;
        }
    }

    // Specific converter for Gender RadioButtons
    public class GenderToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string gender && parameter is string targetGender)
            {
                return string.Equals(gender, targetGender, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter is string targetGender)
            {
                return targetGender;
            }

            return null;
        }
    }
}