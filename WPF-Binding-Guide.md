# WPF Data Binding Copy-Paste Guide

## Basic Binding Syntax

### Simple Property Binding
```xml
<!-- One-way binding (default) -->
<TextBlock Text="{Binding Name}" />
<TextBlock Text="{Binding Student.Name}" />
<TextBlock Text="{Binding SelectedStudent.Email}" />

<!-- Two-way binding -->
<TextBox Text="{Binding Name, Mode=TwoWay}" />
<TextBox Text="{Binding Student.Age, Mode=TwoWay}" />

<!-- One-way to source -->
<TextBox Text="{Binding SearchText, Mode=OneWayToSource}" />

<!-- One-time binding -->
<TextBlock Text="{Binding InitialValue, Mode=OneTime}" />
```

### Binding with UpdateSourceTrigger
```xml
<!-- Update on property change (real-time) -->
<TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />

<!-- Update when focus lost (default) -->
<TextBox Text="{Binding Name, UpdateSourceTrigger=LostFocus}" />

<!-- Update explicitly -->
<TextBox Text="{Binding Value, UpdateSourceTrigger=Explicit}" />
```

## Control-Specific Bindings

### TextBox
```xml
<!-- Basic text binding -->
<TextBox Text="{Binding StudentName, Mode=TwoWay}" />

<!-- Real-time search -->
<TextBox Text="{Binding SearchText, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" 
         x:Name="SearchBox" />

<!-- Placeholder text -->
<TextBox Text="{Binding Email, Mode=TwoWay}">
    <TextBox.Style>
        <Style TargetType="TextBox">
            <Style.Triggers>
                <Trigger Property="Text" Value="">
                    <Setter Property="Background" Value="LightGray" />
                </Trigger>
            </Style.Triggers>
        </Style>
    </TextBox.Style>
</TextBox>
```

### ComboBox
```xml
<!-- Basic ComboBox -->
<ComboBox ItemsSource="{Binding Categories}" 
          SelectedItem="{Binding SelectedCategory, Mode=TwoWay}"
          DisplayMemberPath="Name"
          SelectedValuePath="CategoryId" />

<!-- ComboBox with custom template -->
<ComboBox ItemsSource="{Binding Students}" 
          SelectedItem="{Binding SelectedStudent, Mode=TwoWay}">
    <ComboBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="{Binding Name}" FontWeight="Bold" />
                <TextBlock Text=" - " />
                <TextBlock Text="{Binding Email}" />
            </StackPanel>
        </DataTemplate>
    </ComboBox.ItemTemplate>
</ComboBox>

<!-- ComboBox with SelectedValue -->
<ComboBox ItemsSource="{Binding Categories}"
          SelectedValue="{Binding Student.CategoryId, Mode=TwoWay}"
          DisplayMemberPath="Name"
          SelectedValuePath="CategoryId" />
```

### ListBox / ListView
```xml
<!-- Basic ListBox -->
<ListBox ItemsSource="{Binding Students}" 
         SelectedItem="{Binding SelectedStudent, Mode=TwoWay}"
         DisplayMemberPath="Name" />

<!-- ListBox with custom template -->
<ListBox ItemsSource="{Binding Students}" 
         SelectedItem="{Binding SelectedStudent, Mode=TwoWay}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Margin="5">
                <TextBlock Text="{Binding Name}" FontWeight="Bold" />
                <TextBlock Text="{Binding Email}" Foreground="Gray" />
                <TextBlock Text="{Binding GPA, StringFormat=GPA: {0:F2}}" />
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>

<!-- ListView with columns -->
<ListView ItemsSource="{Binding Students}" 
          SelectedItem="{Binding SelectedStudent, Mode=TwoWay}">
    <ListView.View>
        <GridView>
            <GridViewColumn Header="Name" DisplayMemberBinding="{Binding Name}" Width="150" />
            <GridViewColumn Header="Email" DisplayMemberBinding="{Binding Email}" Width="200" />
            <GridViewColumn Header="GPA" DisplayMemberBinding="{Binding GPA, StringFormat=F2}" Width="80" />
        </GridView>
    </ListView.View>
</ListView>
```

### DataGrid
```xml
<!-- Basic DataGrid -->
<DataGrid ItemsSource="{Binding Students}" 
          SelectedItem="{Binding SelectedStudent, Mode=TwoWay}"
          AutoGenerateColumns="False"
          CanUserAddRows="False">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Name" Binding="{Binding Name}" Width="150" />
        <DataGridTextColumn Header="Email" Binding="{Binding Email}" Width="200" />
        <DataGridTextColumn Header="GPA" Binding="{Binding GPA, StringFormat=F2}" Width="80" />
        <DataGridComboBoxColumn Header="Category" 
                               SelectedItemBinding="{Binding Category}"
                               DisplayMemberPath="Name" />
    </DataGrid.Columns>
</DataGrid>

<!-- DataGrid with template columns -->
<DataGrid ItemsSource="{Binding Students}" 
          SelectedItem="{Binding SelectedStudent, Mode=TwoWay}"
          AutoGenerateColumns="False">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Name" Binding="{Binding Name}" />
        <DataGridTemplateColumn Header="Actions" Width="100">
            <DataGridTemplateColumn.CellTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <Button Content="Edit" Command="{Binding DataContext.EditCommand, RelativeSource={RelativeSource AncestorType=DataGrid}}" 
                                CommandParameter="{Binding}" />
                        <Button Content="Delete" Command="{Binding DataContext.DeleteCommand, RelativeSource={RelativeSource AncestorType=DataGrid}}" 
                                CommandParameter="{Binding}" />
                    </StackPanel>
                </DataTemplate>
            </DataGridTemplateColumn.CellTemplate>
        </DataGridTemplateColumn>
    </DataGrid.Columns>
</DataGrid>
```

### CheckBox
```xml
<!-- Basic CheckBox -->
<CheckBox IsChecked="{Binding IsActive, Mode=TwoWay}" Content="Active" />

<!-- CheckBox with three states -->
<CheckBox IsChecked="{Binding IsSelected, Mode=TwoWay}" IsThreeState="True" Content="Select All" />

<!-- CheckBox list -->
<ItemsControl ItemsSource="{Binding Categories}">
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <CheckBox IsChecked="{Binding IsSelected, Mode=TwoWay}" Content="{Binding Name}" />
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

### RadioButton
```xml
<!-- RadioButton group -->
<StackPanel>
    <RadioButton IsChecked="{Binding Gender, Converter={StaticResource GenderToBooleanConverter}, ConverterParameter=Male}" 
                 Content="Male" GroupName="Gender" />
    <RadioButton IsChecked="{Binding Gender, Converter={StaticResource GenderToBooleanConverter}, ConverterParameter=Female}" 
                 Content="Female" GroupName="Gender" />
</StackPanel>

<!-- RadioButton with enum -->
<StackPanel>
    <RadioButton IsChecked="{Binding Status, Converter={StaticResource EnumToBooleanConverter}, ConverterParameter=Active}" 
                 Content="Active" GroupName="Status" />
    <RadioButton IsChecked="{Binding Status, Converter={StaticResource EnumToBooleanConverter}, ConverterParameter=Inactive}" 
                 Content="Inactive" GroupName="Status" />
</StackPanel>
```

### DatePicker
```xml
<!-- Basic DatePicker -->
<DatePicker SelectedDate="{Binding DateOfBirth, Mode=TwoWay}" />

<!-- DatePicker with DateOnly -->
<DatePicker SelectedDate="{Binding DateOfBirth, Mode=TwoWay, Converter={StaticResource DateOnlyToDateTimeConverter}}" />

<!-- DatePicker with format -->
<DatePicker SelectedDate="{Binding EnrollmentDate, Mode=TwoWay}" 
            SelectedDateFormat="Short" />
```

### Button
```xml
<!-- Button with Command -->
<Button Content="Add Student" Command="{Binding AddStudentCommand}" />

<!-- Button with CommandParameter -->
<Button Content="Edit" Command="{Binding EditStudentCommand}" CommandParameter="{Binding SelectedStudent}" />

<!-- Button with IsEnabled binding -->
<Button Content="Delete" 
        Command="{Binding DeleteStudentCommand}" 
        IsEnabled="{Binding SelectedStudent, Converter={StaticResource NullToBooleanConverter}}" />

<!-- Button in DataTemplate -->
<Button Content="Select" 
        Command="{Binding DataContext.SelectStudentCommand, RelativeSource={RelativeSource AncestorType=Window}}" 
        CommandParameter="{Binding}" />
```

### Image
```xml
<!-- Image with binding -->
<Image Source="{Binding ProfileImagePath}" Width="100" Height="100" />

<!-- Image with default fallback -->
<Image Source="{Binding ProfileImagePath, TargetNullValue=Images/default-profile.png}" />
```

### ProgressBar
```xml
<!-- ProgressBar -->
<ProgressBar Value="{Binding ProgressValue}" Maximum="100" Height="20" />

<!-- ProgressBar with text -->
<Grid>
    <ProgressBar Value="{Binding ProgressValue}" Maximum="100" Height="20" />
    <TextBlock Text="{Binding ProgressValue, StringFormat={}{0}%}" HorizontalAlignment="Center" />
</Grid>
```

### Slider
```xml
<!-- Basic Slider -->
<Slider Value="{Binding GPA, Mode=TwoWay}" Minimum="0" Maximum="4" />

<!-- Slider with tick marks -->
<Slider Value="{Binding Rating, Mode=TwoWay}" 
        Minimum="1" Maximum="5" 
        TickFrequency="1" 
        TickPlacement="BottomRight" />
```

## Advanced Binding Patterns

### Binding with StringFormat
```xml
<!-- Currency formatting -->
<TextBlock Text="{Binding Price, StringFormat=C}" />
<TextBlock Text="{Binding Price, StringFormat=${0:F2}}" />

<!-- Date formatting -->
<TextBlock Text="{Binding DateOfBirth, StringFormat=d}" />
<TextBlock Text="{Binding DateOfBirth, StringFormat=Born: {0:MMMM dd, yyyy}}" />

<!-- Number formatting -->
<TextBlock Text="{Binding GPA, StringFormat=F2}" />
<TextBlock Text="{Binding GPA, StringFormat=GPA: {0:F2}}" />

<!-- Percentage -->
<TextBlock Text="{Binding CompletionRate, StringFormat=P}" />
```

### Binding with Converters
```xml
<!-- Boolean to Visibility -->
<TextBlock Text="Student is active" 
           Visibility="{Binding IsActive, Converter={StaticResource BooleanToVisibilityConverter}}" />

<!-- Inverse Boolean to Visibility -->
<TextBlock Text="Student is inactive" 
           Visibility="{Binding IsActive, Converter={StaticResource BooleanToVisibilityConverter}, ConverterParameter=Inverse}" />

<!-- Date conversion -->
<DatePicker SelectedDate="{Binding DateOfBirth, Converter={StaticResource DateOnlyToDateTimeConverter}}" />

<!-- Number to String -->
<TextBox Text="{Binding Price, Converter={StaticResource NumberToStringConverter}, ConverterParameter=C}" />
```

### Multi-Binding
```xml
<!-- Combine multiple properties -->
<TextBlock>
    <TextBlock.Text>
        <MultiBinding StringFormat="{}{0} - {1}">
            <Binding Path="FirstName" />
            <Binding Path="LastName" />
        </MultiBinding>
    </TextBlock.Text>
</TextBlock>

<!-- Multi-binding with converter -->
<TextBlock>
    <TextBlock.Text>
        <MultiBinding Converter="{StaticResource NameConverter}">
            <Binding Path="FirstName" />
            <Binding Path="LastName" />
        </MultiBinding>
    </TextBlock.Text>
</TextBlock>
```

### Binding with RelativeSource
```xml
<!-- Bind to ancestor -->
<Button Content="Edit" 
        Command="{Binding DataContext.EditCommand, RelativeSource={RelativeSource AncestorType=Window}}" 
        CommandParameter="{Binding}" />

<!-- Bind to self -->
<TextBox Text="{Binding Path=Text, RelativeSource={RelativeSource Self}}" />

<!-- Bind to parent -->
<TextBlock Text="{Binding DataContext.Title, RelativeSource={RelativeSource FindAncestor, AncestorType=UserControl}}" />
```

### Binding with ElementName
```xml
<!-- Bind to another element -->
<TextBox x:Name="SearchBox" Text="{Binding SearchText, Mode=TwoWay}" />
<TextBlock Text="{Binding ElementName=SearchBox, Path=Text}" />

<!-- Bind to slider value -->
<Slider x:Name="FontSlider" Minimum="10" Maximum="30" Value="14" />
<TextBlock Text="Sample Text" FontSize="{Binding ElementName=FontSlider, Path=Value}" />
```

## Collection Binding

### Master-Detail Pattern
```xml
<!-- Master List -->
<ListBox ItemsSource="{Binding Students}" 
         SelectedItem="{Binding SelectedStudent, Mode=TwoWay}"
         DisplayMemberPath="Name" />

<!-- Detail View -->
<StackPanel DataContext="{Binding SelectedStudent}">
    <TextBlock Text="{Binding Name}" FontWeight="Bold" />
    <TextBlock Text="{Binding Email}" />
    <TextBlock Text="{Binding GPA, StringFormat=GPA: {0:F2}}" />
</StackPanel>
```

### Filtered Collections
```xml
<!-- Collection with filter -->
<ListBox ItemsSource="{Binding FilteredStudents}" 
         SelectedItem="{Binding SelectedStudent, Mode=TwoWay}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Name}" />
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>

<!-- Search TextBox -->
<TextBox Text="{Binding SearchText, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" 
         x:Name="SearchBox" />
```

### Grouping
```xml
<!-- ListView with grouping -->
<ListView ItemsSource="{Binding StudentsView}">
    <ListView.GroupStyle>
        <GroupStyle>
            <GroupStyle.HeaderTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Name}" FontWeight="Bold" Background="LightGray" />
                </DataTemplate>
            </GroupStyle.HeaderTemplate>
        </GroupStyle>
    </ListView.GroupStyle>
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Name}" />
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Validation Binding

### IDataErrorInfo Validation
```xml
<!-- TextBox with validation -->
<TextBox Text="{Binding Name, Mode=TwoWay, ValidatesOnDataErrors=True}" />

<!-- Show validation errors -->
<TextBox Text="{Binding Email, Mode=TwoWay, ValidatesOnDataErrors=True}">
    <TextBox.Style>
        <Style TargetType="TextBox">
            <Style.Triggers>
                <Trigger Property="Validation.HasError" Value="True">
                    <Setter Property="Background" Value="LightPink" />
                </Trigger>
            </Style.Triggers>
        </Style>
    </TextBox.Style>
</TextBox>
```

### ValidationRule
```xml
<!-- Custom validation rule -->
<TextBox>
    <TextBox.Text>
        <Binding Path="Age" Mode="TwoWay">
            <Binding.ValidationRules>
                <local:RangeValidationRule Min="18" Max="65" />
            </Binding.ValidationRules>
        </Binding>
    </TextBox.Text>
</TextBox>
```

## Common Binding Scenarios

### Search and Filter
```xml
<!-- Search TextBox -->
<TextBox Text="{Binding SearchText, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" 
         x:Name="SearchBox" />

<!-- Results List -->
<ListBox ItemsSource="{Binding FilteredStudents}" 
         SelectedItem="{Binding SelectedStudent, Mode=TwoWay}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel>
                <TextBlock Text="{Binding Name}" FontWeight="Bold" />
                <TextBlock Text="{Binding Email}" Foreground="Gray" />
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### CRUD Operations
```xml
<!-- Add Form -->
<StackPanel DataContext="{Binding NewStudent}">
    <TextBox Text="{Binding Name, Mode=TwoWay}" />
    <TextBox Text="{Binding Email, Mode=TwoWay}" />
    <Button Content="Add" Command="{Binding DataContext.AddCommand, RelativeSource={RelativeSource AncestorType=Window}}" />
</StackPanel>

<!-- Edit Form -->
<StackPanel DataContext="{Binding SelectedStudent}">
    <TextBox Text="{Binding Name, Mode=TwoWay}" />
    <TextBox Text="{Binding Email, Mode=TwoWay}" />
    <Button Content="Save" Command="{Binding DataContext.SaveCommand, RelativeSource={RelativeSource AncestorType=Window}}" />
</StackPanel>
```

### Dynamic UI
```xml
<!-- Show/Hide based on selection -->
<StackPanel Visibility="{Binding SelectedStudent, Converter={StaticResource NullToVisibilityConverter}}">
    <TextBlock Text="{Binding SelectedStudent.Name}" />
    <TextBlock Text="{Binding SelectedStudent.Email}" />
</StackPanel>

<!-- Enable/Disable based on condition -->
<Button Content="Delete" 
        IsEnabled="{Binding SelectedStudent, Converter={StaticResource NullToBooleanConverter}}"
        Command="{Binding DeleteCommand}" />
```

## Quick Reference

### Common Binding Properties
- `Text="{Binding PropertyName}"` - Basic binding
- `Mode=TwoWay` - Two-way binding
- `UpdateSourceTrigger=PropertyChanged` - Real-time updates
- `StringFormat=C` - Currency formatting
- `Converter={StaticResource ConverterName}` - Use converter
- `ConverterParameter=Value` - Pass parameter to converter

### Common Binding Modes
- `OneWay` (default for most) - Source → Target
- `TwoWay` - Source ↔ Target
- `OneTime` - Source → Target (once)
- `OneWayToSource` - Target → Source

### Common UpdateSourceTrigger Values
- `LostFocus` (default) - When control loses focus
- `PropertyChanged` - On every keystroke
- `Explicit` - Manual update only

Perfect for copy-paste in WPF exams!