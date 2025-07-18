using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WPFBoilerplate.Commands;
using WPFBoilerplate.Models;

namespace WPFBoilerplate.ViewModels
{
    public class MainViewModelWithCommands : BaseViewModel
    {
        private ObservableCollection<Student> _students;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Status> _statusList;
        private Student _selectedStudent;
        private Category _selectedCategory;
        private Status _selectedStatus;
        private string _searchName;
        private string _searchEmail;
        private ICollectionView _studentsView;

        public MainViewModelWithCommands()
        {
            LoadData();
            SetupFiltering();
            InitializeCommands();
        }

        #region Collections
        public ObservableCollection<Student> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public ObservableCollection<Status> StatusList
        {
            get => _statusList;
            set => SetProperty(ref _statusList, value);
        }

        public ICollectionView StudentsView
        {
            get => _studentsView;
            set => SetProperty(ref _studentsView, value);
        }
        #endregion

        #region Selected Items
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => SetProperty(ref _selectedStudent, value);
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set 
            { 
                SetProperty(ref _selectedCategory, value);
                RefreshFilter();
            }
        }

        public Status SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }
        #endregion

        #region Search Properties
        public string SearchName
        {
            get => _searchName;
            set 
            { 
                SetProperty(ref _searchName, value);
                RefreshFilter();
            }
        }

        public string SearchEmail
        {
            get => _searchEmail;
            set 
            { 
                SetProperty(ref _searchEmail, value);
                RefreshFilter();
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddStudent);
            EditCommand = new RelayCommand(EditStudent, () => SelectedStudent != null);
            DeleteCommand = new RelayCommand(DeleteStudent, () => SelectedStudent != null);
            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            RefreshCommand = new RelayCommand(RefreshData);
            SearchCommand = new RelayCommand(ExecuteSearch);
        }
        #endregion

        #region Data Loading
        private void LoadData()
        {
            // Sample data - replace with actual data source
            Students = new ObservableCollection<Student>
            {
                new Student { Id = 1, Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1995, 5, 15), Gender = "Male", Status = "Active", HasScholarship = true },
                new Student { Id = 2, Name = "Jane Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1996, 8, 22), Gender = "Female", Status = "Active", HasScholarship = false },
                new Student { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", DateOfBirth = new DateTime(1994, 12, 10), Gender = "Male", Status = "Inactive", HasScholarship = true },
                new Student { Id = 4, Name = "Alice Brown", Email = "alice@example.com", DateOfBirth = new DateTime(1997, 3, 8), Gender = "Female", Status = "Active", HasScholarship = false },
                new Student { Id = 5, Name = "Charlie Wilson", Email = "charlie@example.com", DateOfBirth = new DateTime(1995, 11, 25), Gender = "Male", Status = "Active", HasScholarship = true }
            };

            Categories = new ObservableCollection<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Computer Science", Description = "Programming and IT" },
                new Category { CategoryId = 2, CategoryName = "Mathematics", Description = "Pure and Applied Math" },
                new Category { CategoryId = 3, CategoryName = "Physics", Description = "Natural Sciences" },
                new Category { CategoryId = 4, CategoryName = "Business", Description = "Business Administration" }
            };

            StatusList = new ObservableCollection<Status>
            {
                new Status { StatusId = 1, StatusName = "Active" },
                new Status { StatusId = 2, StatusName = "Inactive" },
                new Status { StatusId = 3, StatusName = "Graduated" },
                new Status { StatusId = 4, StatusName = "Suspended" }
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

        private void RefreshFilter()
        {
            StudentsView?.Refresh();
        }
        #endregion

        #region Command Implementations
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
                // Edit logic here - usually opens edit form or makes fields editable
                // For now, just a placeholder
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
            // Save logic here - usually saves to database
            // For now, just a placeholder
        }

        private void CancelChanges()
        {
            // Cancel logic here - usually reverts changes
            RefreshData();
        }

        private void RefreshData()
        {
            LoadData();
            RefreshFilter();
        }

        private void ExecuteSearch()
        {
            RefreshFilter();
        }
        #endregion
    }
}