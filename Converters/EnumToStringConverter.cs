using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFBoilerplate.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return enumValue.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && targetType.IsEnum)
            {
                try
                {
                    return Enum.Parse(targetType, stringValue, true);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }

    // Specific converter for Status enum
    public class StatusEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StudentStatus status)
            {
                return status switch
                {
                    StudentStatus.Active => "Active",
                    StudentStatus.Inactive => "Inactive",
                    StudentStatus.Graduated => "Graduated",
                    StudentStatus.Suspended => "Suspended",
                    _ => string.Empty
                };
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.ToLower() switch
                {
                    "active" => StudentStatus.Active,
                    "inactive" => StudentStatus.Inactive,
                    "graduated" => StudentStatus.Graduated,
                    "suspended" => StudentStatus.Suspended,
                    _ => StudentStatus.Active
                };
            }

            return StudentStatus.Active;
        }
    }

    // Sample enum for testing
    public enum StudentStatus
    {
        Active,
        Inactive,
        Graduated,
        Suspended
    }
}