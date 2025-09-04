namespace Student_Management_API.Models;

public class Enrollment
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public int Grade { get; set; }

    public enum Status
    {
        Enrolled,
        Completed,
        Dropped
    } 
    public Status EnrollmentStatus { get; set; }
    
    //Foreign Keys
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    
    //Navigation Properties
    public virtual Student? Student { get; set; }
    public virtual Course? Course { get; set; }
}