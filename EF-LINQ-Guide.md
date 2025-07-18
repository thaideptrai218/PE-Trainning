# Entity Framework & LINQ Copy-Paste Guide

## DbContext Setup Templates

### Basic DbContext
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=ExamDB;Trusted_Connection=true;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId });

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        // Configure decimal precision
        modelBuilder.Entity<Student>()
            .Property(s => s.GPA)
            .HasPrecision(3, 2);

        // Configure string lengths
        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .HasMaxLength(100);
    }
}
```

### Entity Models with Navigation Properties
```csharp
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal GPA { get; set; }
    public int? CategoryId { get; set; }

    // Navigation properties
    public Category Category { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public decimal Price { get; set; }
    public int InstructorId { get; set; }

    // Navigation properties
    public Instructor Instructor { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

public class Enrollment
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public decimal? Grade { get; set; }

    // Navigation properties
    public Student Student { get; set; }
    public Course Course { get; set; }
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public ICollection<Student> Students { get; set; } = new List<Student>();
}

public class Instructor
{
    public int InstructorId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }

    // Navigation properties
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
```

## LINQ Query Templates

### Basic Queries
```csharp
using var context = new AppDbContext();

// Get all students
var allStudents = context.Students.ToList();

// Get student by ID
var student = context.Students.Find(1);

// Get students by condition
var activeStudents = context.Students
    .Where(s => s.GPA >= 3.0)
    .ToList();

// Get first student matching condition
var topStudent = context.Students
    .Where(s => s.GPA >= 3.5)
    .FirstOrDefault();

// Check if any student exists
var hasStudents = context.Students.Any();

// Count students
var studentCount = context.Students.Count();

// Count students with condition
var highGPACount = context.Students
    .Count(s => s.GPA >= 3.5);
```

### Filtering and Searching
```csharp
// Search by name (contains)
var searchResults = context.Students
    .Where(s => s.Name.Contains("John"))
    .ToList();

// Search by email (starts with)
var emailSearch = context.Students
    .Where(s => s.Email.StartsWith("john"))
    .ToList();

// Multiple conditions
var filteredStudents = context.Students
    .Where(s => s.GPA >= 3.0 && s.DateOfBirth.Year >= 2000)
    .ToList();

// Date range filtering
var recentStudents = context.Students
    .Where(s => s.DateOfBirth >= DateOnly.FromDateTime(DateTime.Now.AddYears(-25)))
    .ToList();

// Null checking
var studentsWithCategory = context.Students
    .Where(s => s.CategoryId != null)
    .ToList();
```

### Sorting and Ordering
```csharp
// Order by single field
var sortedByName = context.Students
    .OrderBy(s => s.Name)
    .ToList();

// Order by multiple fields
var sortedStudents = context.Students
    .OrderBy(s => s.CategoryId)
    .ThenByDescending(s => s.GPA)
    .ToList();

// Order by navigation property
var sortedByCategory = context.Students
    .Include(s => s.Category)
    .OrderBy(s => s.Category.Name)
    .ToList();
```

### Navigation Properties and Include
```csharp
// Load related data
var studentsWithCategories = context.Students
    .Include(s => s.Category)
    .ToList();

// Load multiple related entities
var studentsWithEnrollments = context.Students
    .Include(s => s.Category)
    .Include(s => s.Enrollments)
    .ToList();

// Load nested related data
var studentsWithCourses = context.Students
    .Include(s => s.Enrollments)
        .ThenInclude(e => e.Course)
    .ToList();

// Load multiple levels
var fullStudentData = context.Students
    .Include(s => s.Category)
    .Include(s => s.Enrollments)
        .ThenInclude(e => e.Course)
            .ThenInclude(c => c.Instructor)
    .ToList();
```

### Grouping and Aggregation
```csharp
// Group by category
var studentsByCategory = context.Students
    .GroupBy(s => s.CategoryId)
    .Select(g => new
    {
        CategoryId = g.Key,
        Count = g.Count(),
        AverageGPA = g.Average(s => s.GPA)
    })
    .ToList();

// Group by date
var studentsByYear = context.Students
    .GroupBy(s => s.DateOfBirth.Year)
    .Select(g => new
    {
        Year = g.Key,
        Count = g.Count()
    })
    .ToList();

// Aggregate functions
var statistics = context.Students
    .GroupBy(s => 1)
    .Select(g => new
    {
        TotalStudents = g.Count(),
        AverageGPA = g.Average(s => s.GPA),
        MaxGPA = g.Max(s => s.GPA),
        MinGPA = g.Min(s => s.GPA)
    })
    .FirstOrDefault();
```

### Joining Tables
```csharp
// Inner join
var studentsWithCourses = context.Students
    .Join(context.Enrollments,
          student => student.StudentId,
          enrollment => enrollment.StudentId,
          (student, enrollment) => new { student, enrollment })
    .Join(context.Courses,
          se => se.enrollment.CourseId,
          course => course.CourseId,
          (se, course) => new
          {
              StudentName = se.student.Name,
              CourseName = course.Title,
              Credits = course.Credits
          })
    .ToList();

// Left join using navigation properties
var studentsWithOptionalEnrollments = context.Students
    .SelectMany(s => s.Enrollments.DefaultIfEmpty(),
                (student, enrollment) => new
                {
                    StudentName = student.Name,
                    CourseName = enrollment != null ? enrollment.Course.Title : "No Courses"
                })
    .ToList();
```

### Complex Queries
```csharp
// Students with more than 3 courses
var activeStudents = context.Students
    .Where(s => s.Enrollments.Count > 3)
    .ToList();

// Students with high GPA in specific courses
var topStudentsInMath = context.Students
    .Where(s => s.GPA >= 3.5 && 
                s.Enrollments.Any(e => e.Course.Title.Contains("Math")))
    .ToList();

// Students enrolled in courses by specific instructor
var studentsOfInstructor = context.Students
    .Where(s => s.Enrollments
        .Any(e => e.Course.Instructor.Name == "John Smith"))
    .ToList();

// Average GPA by category
var avgGPAByCategory = context.Categories
    .Select(c => new
    {
        CategoryName = c.Name,
        AverageGPA = c.Students.Average(s => s.GPA),
        StudentCount = c.Students.Count()
    })
    .ToList();
```

### Projection and Anonymous Types
```csharp
// Select specific fields
var studentSummary = context.Students
    .Select(s => new
    {
        s.Name,
        s.Email,
        s.GPA,
        CategoryName = s.Category.Name
    })
    .ToList();

// Calculate derived fields
var studentAges = context.Students
    .Select(s => new
    {
        s.Name,
        Age = DateTime.Now.Year - s.DateOfBirth.Year,
        s.GPA
    })
    .ToList();

// Conditional projection
var studentStatus = context.Students
    .Select(s => new
    {
        s.Name,
        Status = s.GPA >= 3.0 ? "Good Standing" : "Needs Improvement",
        CourseCount = s.Enrollments.Count()
    })
    .ToList();
```

## CRUD Operations

### Create
```csharp
// Add single entity
var newStudent = new Student
{
    Name = "John Doe",
    Email = "john@example.com",
    DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
    GPA = 3.5m,
    CategoryId = 1
};

context.Students.Add(newStudent);
context.SaveChanges();

// Add multiple entities
var newStudents = new List<Student>
{
    new Student { Name = "Jane Smith", Email = "jane@example.com", GPA = 3.8m },
    new Student { Name = "Bob Johnson", Email = "bob@example.com", GPA = 3.2m }
};

context.Students.AddRange(newStudents);
context.SaveChanges();
```

### Read
```csharp
// Find by primary key
var student = context.Students.Find(1);

// Find with navigation properties
var studentWithCategory = context.Students
    .Include(s => s.Category)
    .FirstOrDefault(s => s.StudentId == 1);

// Find multiple
var students = context.Students
    .Where(s => s.GPA >= 3.0)
    .Include(s => s.Category)
    .ToList();
```

### Update
```csharp
// Update single entity
var student = context.Students.Find(1);
if (student != null)
{
    student.GPA = 3.8m;
    student.Email = "newemail@example.com";
    context.SaveChanges();
}

// Update multiple entities
var studentsToUpdate = context.Students
    .Where(s => s.CategoryId == 1)
    .ToList();

foreach (var student in studentsToUpdate)
{
    student.GPA += 0.1m;
}
context.SaveChanges();
```

### Delete
```csharp
// Delete single entity
var student = context.Students.Find(1);
if (student != null)
{
    context.Students.Remove(student);
    context.SaveChanges();
}

// Delete multiple entities
var studentsToDelete = context.Students
    .Where(s => s.GPA < 2.0)
    .ToList();

context.Students.RemoveRange(studentsToDelete);
context.SaveChanges();
```

## Common Exam Patterns

### Repository Pattern
```csharp
public interface IStudentRepository
{
    List<Student> GetAllStudents();
    Student GetStudentById(int id);
    List<Student> SearchStudents(string searchTerm);
    void AddStudent(Student student);
    void UpdateStudent(Student student);
    void DeleteStudent(int id);
}

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Student> GetAllStudents()
    {
        return _context.Students
            .Include(s => s.Category)
            .ToList();
    }

    public Student GetStudentById(int id)
    {
        return _context.Students
            .Include(s => s.Category)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefault(s => s.StudentId == id);
    }

    public List<Student> SearchStudents(string searchTerm)
    {
        return _context.Students
            .Where(s => s.Name.Contains(searchTerm) || 
                       s.Email.Contains(searchTerm))
            .Include(s => s.Category)
            .ToList();
    }

    public void AddStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void DeleteStudent(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
```

### Service Layer Pattern
```csharp
public class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<Student> GetStudentsByCategory(int categoryId)
    {
        return _repository.GetAllStudents()
            .Where(s => s.CategoryId == categoryId)
            .ToList();
    }

    public List<Student> GetTopStudents(int count)
    {
        return _repository.GetAllStudents()
            .OrderByDescending(s => s.GPA)
            .Take(count)
            .ToList();
    }

    public bool IsEmailUnique(string email, int? excludeStudentId = null)
    {
        var students = _repository.GetAllStudents();
        return !students.Any(s => s.Email == email && s.StudentId != excludeStudentId);
    }
}
```

## Quick Reference

### Most Common LINQ Methods
- `Where()` - Filtering
- `Select()` - Projection
- `OrderBy()` / `OrderByDescending()` - Sorting
- `Include()` - Load related data
- `FirstOrDefault()` - Get first match
- `Any()` - Check existence
- `Count()` - Count items
- `GroupBy()` - Group data
- `Join()` - Join tables
- `ToList()` - Execute query

### Most Common EF Operations
- `Find()` - Get by primary key
- `Add()` / `AddRange()` - Insert
- `Update()` - Modify
- `Remove()` / `RemoveRange()` - Delete
- `SaveChanges()` - Commit changes

Perfect for copy-paste in exam scenarios!