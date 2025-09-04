using Microsoft.EntityFrameworkCore;
using Student_Management_API.Models;

namespace Student_Management_API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}