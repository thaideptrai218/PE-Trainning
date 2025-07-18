using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFBoilerplate.Converters
{
    public class NumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            // Apply formatting if parameter is provided
            if (parameter is string format && !string.IsNullOrEmpty(format))
            {
                if (value is IFormattable formattable)
                {
                    return formattable.ToString(format, culture);
                }
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (string.IsNullOrWhiteSpace(stringValue))
                    return null;

                try
                {
                    if (targetType == typeof(int) || targetType == typeof(int?))
                    {
                        return int.Parse(stringValue);
                    }
                    else if (targetType == typeof(double) || targetType == typeof(double?))
                    {
                        return double.Parse(stringValue);
                    }
                    else if (targetType == typeof(decimal) || targetType == typeof(decimal?))
                    {
                        return decimal.Parse(stringValue);
                    }
                    else if (targetType == typeof(float) || targetType == typeof(float?))
                    {
                        return float.Parse(stringValue);
                    }
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}