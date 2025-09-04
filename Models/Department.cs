namespace Student_Management_API.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string ManagerName { get; set; } = String.Empty;
    
    //Navigation Properties
    public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

}