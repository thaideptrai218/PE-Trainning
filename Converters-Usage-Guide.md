# WPF Converters Copy-Paste Guide

## How to Use Converters

### 1. Add Converters to Window Resources
```xml
<Window.Resources>
    <local:DateOnlyToDateTimeConverter x:Key="DateOnlyToDateTimeConverter" />
    <local:BooleanToVisibilityConverter x:Key="BooleanToVisibilityConverter" />
    <local:GenderToBooleanConverter x:Key="GenderToBooleanConverter" />
    <local:StringToBooleanConverter x:Key="StringToBooleanConverter" />
    <local:EnumToStringConverter x:Key="EnumToStringConverter" />
    <local:NullToBooleanConverter x:Key="NullToBooleanConverter" />
    <local:InvertBooleanConverter x:Key="InvertBooleanConverter" />
    <local:NumberToStringConverter x:Key="NumberToStringConverter" />
    <local:BooleanToStringConverter x:Key="BooleanToStringConverter" />
</Window.Resources>
```

## Converter Templates

### 1. DateOnlyToDateTimeConverter
```csharp
using System;
using System.Globalization;
using System.Windows.Data;

public class DateOnlyToDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            return dateOnly.ToDateTime(TimeOnly.MinValue);
        }
        
        if (value is DateTime dateTime)
        {
            return dateTime;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        return null;
    }
}
```

**Usage:**
```xml
<DatePicker SelectedDate="{Binding DateOfBirth, Converter={StaticResource DateOnlyToDateTimeConverter}}" />
```

### 2. BooleanToVisibilityConverter
```csharp
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
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
```

**Usage:**
```xml
<!-- Show element when IsActive is true -->
<TextBlock Text="Active Student" Visibility="{Binding IsActive, Converter={StaticResource BooleanToVisibilityConverter}}" />

<!-- Hide element when IsActive is true (inverse) -->
<TextBlock Text="Inactive Student" Visibility="{Binding IsActive, Converter={StaticResource BooleanToVisibilityConverter}, ConverterParameter=Inverse}" />
```

### 3. GenderToBooleanConverter (for RadioButtons)
```csharp
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
```

**Usage:**
```xml
<RadioButton Content="Male" GroupName="Gender" 
             IsChecked="{Binding SelectedStudent.Gender, Converter={StaticResource GenderToBooleanConverter}, ConverterParameter=Male}" />
<RadioButton Content="Female" GroupName="Gender" 
             IsChecked="{Binding SelectedStudent.Gender, Converter={StaticResource GenderToBooleanConverter}, ConverterParameter=Female}" />
```

### 4. StringToBooleanConverter
```csharp
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
```

**Usage:**
```xml
<RadioButton Content="Active" GroupName="Status" 
             IsChecked="{Binding SelectedStudent.Status, Converter={StaticResource StringToBooleanConverter}, ConverterParameter=Active}" />
<RadioButton Content="Inactive" GroupName="Status" 
             IsChecked="{Binding SelectedStudent.Status, Converter={StaticResource StringToBooleanConverter}, ConverterParameter=Inactive}" />
```

### 5. EnumToStringConverter
```csharp
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
```

**Usage:**
```xml
<ComboBox ItemsSource="{Binding StatusEnumValues}" 
          SelectedItem="{Binding SelectedStatus, Converter={StaticResource EnumToStringConverter}}" />
```

### 6. NullToBooleanConverter
```csharp
public class NullToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
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
```

**Usage:**
```xml
<!-- Enable button when SelectedStudent is not null -->
<Button Content="Edit" IsEnabled="{Binding SelectedStudent, Converter={StaticResource NullToBooleanConverter}}" />

<!-- Enable button when SelectedStudent is null -->
<Button Content="Add" IsEnabled="{Binding SelectedStudent, Converter={StaticResource NullToBooleanConverter}, ConverterParameter=Inverse}" />
```

### 7. InvertBooleanConverter
```csharp
public class InvertBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        return false;
    }
}
```

**Usage:**
```xml
<CheckBox Content="Is Inactive" IsChecked="{Binding IsActive, Converter={StaticResource InvertBooleanConverter}}" />
```

### 8. NumberToStringConverter
```csharp
public class NumberToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return string.Empty;

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
            }
            catch
            {
                return null;
            }
        }

        return null;
    }
}
```

**Usage:**
```xml
<!-- Format currency -->
<TextBox Text="{Binding Price, Converter={StaticResource NumberToStringConverter}, ConverterParameter=C}" />

<!-- Format percentage -->
<TextBox Text="{Binding Percentage, Converter={StaticResource NumberToStringConverter}, ConverterParameter=P}" />

<!-- Format with 2 decimal places -->
<TextBox Text="{Binding Amount, Converter={StaticResource NumberToStringConverter}, ConverterParameter=F2}" />
```

## Quick Reference

### Common Converter Usage Patterns

#### DatePicker with DateOnly
```xml
<DatePicker SelectedDate="{Binding DateOfBirth, Converter={StaticResource DateOnlyToDateTimeConverter}}" />
```

#### RadioButtons for Gender
```xml
<RadioButton Content="Male" GroupName="Gender" 
             IsChecked="{Binding Gender, Converter={StaticResource GenderToBooleanConverter}, ConverterParameter=Male}" />
<RadioButton Content="Female" GroupName="Gender" 
             IsChecked="{Binding Gender, Converter={StaticResource GenderToBooleanConverter}, ConverterParameter=Female}" />
```

#### RadioButtons for Status
```xml
<RadioButton Content="Active" GroupName="Status" 
             IsChecked="{Binding Status, Converter={StaticResource StringToBooleanConverter}, ConverterParameter=Active}" />
<RadioButton Content="Inactive" GroupName="Status" 
             IsChecked="{Binding Status, Converter={StaticResource StringToBooleanConverter}, ConverterParameter=Inactive}" />
```

#### Show/Hide Elements
```xml
<TextBlock Text="Has Scholarship" Visibility="{Binding HasScholarship, Converter={StaticResource BooleanToVisibilityConverter}}" />
<TextBlock Text="No Scholarship" Visibility="{Binding HasScholarship, Converter={StaticResource BooleanToVisibilityConverter}, ConverterParameter=Inverse}" />
```

#### Enable/Disable Buttons
```xml
<Button Content="Edit" IsEnabled="{Binding SelectedItem, Converter={StaticResource NullToBooleanConverter}}" />
<Button Content="Delete" IsEnabled="{Binding SelectedItem, Converter={StaticResource NullToBooleanConverter}}" />
```

### 9. BooleanToStringConverter
```csharp
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
```

**Usage:**
```xml
<!-- Default Yes/No -->
<TextBlock Text="{Binding IsActive, Converter={StaticResource BooleanToStringConverter}}" />

<!-- Custom strings -->
<TextBlock Text="{Binding IsActive, Converter={StaticResource BooleanToStringConverter}, ConverterParameter=Active|Inactive}" />
<TextBlock Text="{Binding HasPermission, Converter={StaticResource BooleanToStringConverter}, ConverterParameter=Allowed|Denied}" />
<TextBlock Text="{Binding IsOnline, Converter={StaticResource BooleanToStringConverter}, ConverterParameter=Online|Offline}" />
```

### Setup Steps

1. **Add using statement**: `xmlns:local="clr-namespace:YourNamespace.Converters"`
2. **Add to Window.Resources**: Copy converter resources
3. **Use in bindings**: Add `Converter={StaticResource ConverterName}`
4. **Add parameters**: Use `ConverterParameter=Value` when needed

Perfect for exam scenarios - just copy the converter you need and use it!