using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFBoilerplate.Converters
{
    public class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Check if parameter contains custom strings
                if (parameter is string paramString && paramString.Contains("|"))
                {
                    string[] options = paramString.Split('|');
                    if (options.Length == 2)
                    {
                        return boolValue ? options[0] : options[1];
                    }
                }
                
                // Default Yes/No
                return boolValue ? "Yes" : "No";
            }

            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                // Check if parameter contains custom strings
                if (parameter is string paramString && paramString.Contains("|"))
                {
                    string[] options = paramString.Split('|');
                    if (options.Length == 2)
                    {
                        return string.Equals(stringValue, options[0], StringComparison.OrdinalIgnoreCase);
                    }
                }
                
                // Default Yes/No conversion
                return string.Equals(stringValue, "Yes", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}