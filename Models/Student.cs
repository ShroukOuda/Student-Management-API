using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Student_Management_API.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public DateTime BirthDate { get; set; }
    
    [Precision(3,2)]
    public decimal GPA { get; set; }
    public string Phone { get; set; } = String.Empty;
    public string Address { get; set; } =String.Empty;
    public DateTime EnrollmentDate { get; set; } 
    
    //Foreign Key
    public int DepartmentId { get; set; }
    
    //Navigation Properties
    [ForeignKey("DepartmentId")]
    public virtual Department? Department { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

}