namespace Student_Management_API.Services;

public interface ICoursesService
{
    Task<IEnumerable<Course>> GetAll();
    Task<Course> GetById(int Id);
    Task<Course> CreateCourse(Course course);
    Course UpdateCourse(Course course);
    Course DeleteCourse(Course course);

}