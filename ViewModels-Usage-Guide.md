# WPF ViewModels Usage Guide

## Quick Copy-Paste ViewModels for WPF Templates

### 1. BaseViewModel (Foundation)
```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

### 2. Model Classes

#### Student Model
```csharp
public class Student : INotifyPropertyChanged
{
    private int _id;
    private string _name;
    private string _email;
    private DateTime _dateOfBirth;
    private string _gender;
    private string _status;
    private bool _hasScholarship;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => SetProperty(ref _dateOfBirth, value);
    }

    public string Gender
    {
        get => _gender;
        set => SetProperty(ref _gender, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public bool HasScholarship
    {
        get => _hasScholarship;
        set => SetProperty(ref _hasScholarship, value);
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value)) return false;
        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

#### Category Model
```csharp
public class Category : INotifyPropertyChanged
{
    private int _categoryId;
    private string _categoryName;
    private string _description;

    public int CategoryId
    {
        get => _categoryId;
        set => SetProperty(ref _categoryId, value);
    }

    public string CategoryName
    {
        get => _categoryName;
        set => SetProperty(ref _categoryName, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    // INotifyPropertyChanged implementation (same as above)
}
```

### 3. RelayCommand (for Button Commands)
```csharp
using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
    public void Execute(object parameter) => _execute();
}
```

### 4. MainViewModel (Complete with Collections and Commands)
```csharp
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

public class MainViewModel : BaseViewModel
{
    private ObservableCollection<Student> _students;
    private Student _selectedStudent;
    private string _searchName;
    private string _searchEmail;
    private ICollectionView _studentsView;

    public MainViewModel()
    {
        LoadData();
        SetupFiltering();
        InitializeCommands();
    }

    #region Collections and Properties
    public ObservableCollection<Student> Students
    {
        get => _students;
        set => SetProperty(ref _students, value);
    }

    public Student SelectedStudent
    {
        get => _selectedStudent;
        set => SetProperty(ref _selectedStudent, value);
    }

    public string SearchName
    {
        get => _searchName;
        set 
        { 
            SetProperty(ref _searchName, value);
            StudentsView?.Refresh();
        }
    }

    public string SearchEmail
    {
        get => _searchEmail;
        set 
        { 
            SetProperty(ref _searchEmail, value);
            StudentsView?.Refresh();
        }
    }

    public ICollectionView StudentsView
    {
        get => _studentsView;
        set => SetProperty(ref _studentsView, value);
    }
    #endregion

    #region Commands
    public ICommand AddCommand { get; private set; }
    public ICommand EditCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }
    public ICommand SaveCommand { get; private set; }
    public ICommand RefreshCommand { get; private set; }

    private void InitializeCommands()
    {
        AddCommand = new RelayCommand(AddStudent);
        EditCommand = new RelayCommand(EditStudent, () => SelectedStudent != null);
        DeleteCommand = new RelayCommand(DeleteStudent, () => SelectedStudent != null);
        SaveCommand = new RelayCommand(SaveChanges);
        RefreshCommand = new RelayCommand(RefreshData);
    }
    #endregion

    #region Data and Filtering
    private void LoadData()
    {
        Students = new ObservableCollection<Student>
        {
            new Student { Id = 1, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1995, 5, 15), Gender = "Male", Status = "Active", HasScholarship = true },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1996, 8, 22), Gender = "Female", Status = "Active", HasScholarship = false }
        };
    }

    private void SetupFiltering()
    {
        StudentsView = CollectionViewSource.GetDefaultView(Students);
        StudentsView.Filter = FilterStudents;
    }

    private bool FilterStudents(object item)
    {
        if (item is Student student)
        {
            bool nameMatch = string.IsNullOrEmpty(SearchName) || 
                            student.Name.Contains(SearchName, StringComparison.OrdinalIgnoreCase);
            
            bool emailMatch = string.IsNullOrEmpty(SearchEmail) || 
                             student.Email.Contains(SearchEmail, StringComparison.OrdinalIgnoreCase);

            return nameMatch && emailMatch;
        }
        return false;
    }
    #endregion

    #region CRUD Operations
    private void AddStudent()
    {
        var newStudent = new Student
        {
            Id = Students.Count + 1,
            Name = "New Student",
            Email = "new@example.com",
            DateOfBirth = DateTime.Now.AddYears(-20),
            Gender = "Male",
            Status = "Active",
            HasScholarship = false
        };
        Students.Add(newStudent);
        SelectedStudent = newStudent;
    }

    private void EditStudent()
    {
        if (SelectedStudent != null)
        {
            // Edit logic here
        }
    }

    private void DeleteStudent()
    {
        if (SelectedStudent != null)
        {
            Students.Remove(SelectedStudent);
            SelectedStudent = null;
        }
    }

    private void SaveChanges()
    {
        // Save to database logic here
    }

    private void RefreshData()
    {
        LoadData();
        StudentsView?.Refresh();
    }
    #endregion
}
```

### 5. How to Use in MainWindow.xaml.cs
```csharp
using System.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
```

### 6. Key XAML Bindings for Templates

#### DataGrid Binding
```xml
<DataGrid ItemsSource="{Binding StudentsView}"
          SelectedItem="{Binding SelectedStudent, Mode=TwoWay}">
    <DataGrid.Columns>
        <DataGridTextColumn Binding="{Binding Id}" Header="ID" />
        <DataGridTextColumn Binding="{Binding Name}" Header="Name" />
        <DataGridTextColumn Binding="{Binding Email}" Header="Email" />
    </DataGrid.Columns>
</DataGrid>
```

#### Search TextBox Binding
```xml
<TextBox Text="{Binding SearchName, UpdateSourceTrigger=PropertyChanged}" />
<TextBox Text="{Binding SearchEmail, UpdateSourceTrigger=PropertyChanged}" />
```

#### Form Controls Binding
```xml
<TextBox Text="{Binding SelectedStudent.Name, UpdateSourceTrigger=PropertyChanged}" />
<TextBox Text="{Binding SelectedStudent.Email, UpdateSourceTrigger=PropertyChanged}" />
<DatePicker SelectedDate="{Binding SelectedStudent.DateOfBirth, UpdateSourceTrigger=PropertyChanged}" />
<CheckBox IsChecked="{Binding SelectedStudent.HasScholarship, UpdateSourceTrigger=PropertyChanged}" />
```

#### Button Command Binding
```xml
<Button Content="Add" Command="{Binding AddCommand}" />
<Button Content="Edit" Command="{Binding EditCommand}" />
<Button Content="Delete" Command="{Binding DeleteCommand}" />
<Button Content="Save" Command="{Binding SaveCommand}" />
<Button Content="Refresh" Command="{Binding RefreshCommand}" />
```

### 7. Quick Setup Steps

1. **Create Models** - Copy Student, Category, Status models
2. **Create BaseViewModel** - Copy BaseViewModel class
3. **Create RelayCommand** - Copy RelayCommand class
4. **Create MainViewModel** - Copy MainViewModel, replace Student with your model
5. **Set DataContext** - In MainWindow.xaml.cs: `DataContext = new MainViewModel();`
6. **Update Bindings** - Replace property names in XAML with your model properties

### 8. Common Patterns for Different Data Types

#### For Employee Data:
```csharp
public class Employee : INotifyPropertyChanged
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
}
```

#### For Product Data:
```csharp
public class Product : INotifyPropertyChanged
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; }
    public bool IsDiscontinued { get; set; }
}
```

Just replace the model properties in your ViewModel and XAML bindings!