using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_API.Models;

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Prerequisites { get; set; } = String.Empty;
    public string InstructorName { get; set; } = String.Empty;
    public int Credits { get; set; }
    public int MaxEnrollment { get; set; }
    
    //Foreign Key
    public int DepartmentId { get; set; }
    
    //Navigation Properties
    [ForeignKey("DepartmentId")]
    public virtual Department? Department { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
}