# WPF Boilerplate Repository

A comprehensive WPF boilerplate collection for practical exams and rapid development. This repository contains ready-to-use templates, ViewModels, converters, and guides for common WPF scenarios.

## 🚀 Quick Start

1. **Clone the repository**
2. **Copy the files you need** to your WPF project
3. **Update namespaces** to match your project
4. **Follow the usage guides** for implementation

## 📁 Repository Structure

```
WPF-Boilerplate/
├── Commands/                     # Command implementations
│   └── RelayCommand.cs          # ICommand implementation
├── Converters/                   # Value converters
│   ├── BooleanToStringConverter.cs
│   ├── BooleanToVisibilityConverter.cs
│   ├── DateOnlyToDateTimeConverter.cs
│   ├── EnumToStringConverter.cs
│   ├── GenderToBooleanConverter.cs
│   ├── InvertBooleanConverter.cs
│   ├── NullToBooleanConverter.cs
│   ├── NumberToStringConverter.cs
│   └── StringToBooleanConverter.cs
├── Models/                       # Data models
│   ├── Category.cs
│   ├── Status.cs
│   └── Student.cs
├── ViewModels/                   # MVVM ViewModels
│   ├── BaseViewModel.cs
│   ├── MainViewModel.cs
│   └── MainViewModelWithCommands.cs
├── MainWindow.xaml              # Example implementation
├── MainWindow.xaml.cs           # Code-behind
├── UniversalWPFBoilerplate.xaml # ResourceDictionary templates
└── *.md                         # Usage guides
```

## 📚 Usage Guides

### Essential Guides
- **[WPF_Controls_Guide.md](WPF_Controls_Guide.md)** - Copy-paste XAML templates
- **[ViewModels-Usage-Guide.md](ViewModels-Usage-Guide.md)** - MVVM implementation
- **[Converters-Usage-Guide.md](Converters-Usage-Guide.md)** - Value converters
- **[WPF-Binding-Guide.md](WPF-Binding-Guide.md)** - Data binding examples
- **[EF-LINQ-Guide.md](EF-LINQ-Guide.md)** - Entity Framework & LINQ

## 🎯 For Practical Exams

### Step 1: Set Up Your Project
```xml
<!-- Add to App.xaml -->
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="UniversalWPFBoilerplate.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### Step 2: Copy Base Components
1. Copy `BaseViewModel.cs` to your ViewModels folder
2. Copy `RelayCommand.cs` to your Commands folder
3. Copy needed converters to your Converters folder

### Step 3: Create Your ViewModel
```csharp
public class MainViewModel : BaseViewModel
{
    // Copy from MainViewModelWithCommands.cs
    // Replace Student with your model class
}
```

### Step 4: Set DataContext
```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
```

## 🔧 Common Patterns

### Master-Detail Layout
```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="60*" />
        <ColumnDefinition Width="40*" />
    </Grid.ColumnDefinitions>
    
    <!-- Master: DataGrid -->
    <DataGrid Grid.Column="0" ItemsSource="{Binding Items}" />
    
    <!-- Detail: Form -->
    <GroupBox Grid.Column="1" Header="Details">
        <!-- Form controls -->
    </GroupBox>
</Grid>
```

### Search and Filter
```xml
<TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />
<DataGrid ItemsSource="{Binding FilteredItems}" />
```

### CRUD Operations
```xml
<Button Content="Add" Command="{Binding AddCommand}" />
<Button Content="Edit" Command="{Binding EditCommand}" />
<Button Content="Delete" Command="{Binding DeleteCommand}" />
```

## 📋 Available Templates

### Controls
- DataGrid with columns
- ComboBox with binding
- TextBox with validation
- RadioButton groups
- CheckBox binding
- DatePicker with DateOnly
- Button with commands

### Converters
- Boolean ↔ String
- Boolean ↔ Visibility
- DateOnly ↔ DateTime
- String ↔ Boolean (RadioButtons)
- Enum ↔ String
- Null ↔ Boolean
- Number ↔ String formatting

### ViewModels
- BaseViewModel with INotifyPropertyChanged
- MainViewModel with collections
- Command implementations
- Search/filter functionality
- CRUD operations

## 🎨 Example Implementation

### 1. Models
```csharp
public class Student : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    // ... INotifyPropertyChanged implementation
}
```

### 2. ViewModel
```csharp
public class MainViewModel : BaseViewModel
{
    public ObservableCollection<Student> Students { get; set; }
    public Student SelectedStudent { get; set; }
    public string SearchText { get; set; }
    
    public ICommand AddCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
}
```

### 3. XAML
```xml
<Grid>
    <!-- Search -->
    <TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />
    
    <!-- DataGrid -->
    <DataGrid ItemsSource="{Binding Students}" 
              SelectedItem="{Binding SelectedStudent, Mode=TwoWay}" />
    
    <!-- Buttons -->
    <Button Content="Add" Command="{Binding AddCommand}" />
</Grid>
```

## 🔗 Entity Framework Integration

### DbContext Setup
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionString");
    }
}
```

### LINQ Queries
```csharp
// Filter
var students = context.Students.Where(s => s.Name.Contains(searchText)).ToList();

// Include navigation
var studentsWithCategories = context.Students.Include(s => s.Category).ToList();

// Group by
var grouped = context.Students.GroupBy(s => s.CategoryId).ToList();
```

## 🎯 Exam Tips

1. **Start with Master-Detail layout** - Most exams use this pattern
2. **Use UpdateSourceTrigger=PropertyChanged** for real-time search
3. **Always implement INotifyPropertyChanged** in models
4. **Use converters for RadioButton binding** (Gender, Status)
5. **DateOnly needs converter** for DatePicker
6. **Enable/disable buttons** based on selection

## 📖 Quick Reference

### Most Used Bindings
```xml
<!-- Text controls -->
Text="{Binding Property, Mode=TwoWay}"
Text="{Binding Property, UpdateSourceTrigger=PropertyChanged}"

<!-- Collections -->
ItemsSource="{Binding Collection}"
SelectedItem="{Binding SelectedItem, Mode=TwoWay}"

<!-- Buttons -->
Command="{Binding CommandName}"
IsEnabled="{Binding SelectedItem, Converter={StaticResource NullToBooleanConverter}}"

<!-- Visibility -->
Visibility="{Binding IsVisible, Converter={StaticResource BooleanToVisibilityConverter}}"
```

### Common Converters
- `BooleanToVisibilityConverter` - Show/hide elements
- `GenderToBooleanConverter` - RadioButton binding
- `DateOnlyToDateTimeConverter` - DatePicker compatibility
- `BooleanToStringConverter` - Display Yes/No, Active/Inactive

## 🚀 Getting Started Template

1. Copy `BaseViewModel.cs` and `RelayCommand.cs`
2. Create your model class with INotifyPropertyChanged
3. Create ViewModel inheriting from BaseViewModel
4. Set up master-detail XAML layout
5. Add converters as needed
6. Implement CRUD operations

Perfect for WPF practical exams! 🎉

## 📝 License

This is an educational resource for WPF development and practical exams. Use freely for learning and development purposes.

## 🤝 Contributing

Feel free to add more templates, converters, or improve existing ones. This is a community resource for WPF developers and students.