namespace Student_Management_API.Services;

public interface IStudentsService
{
    Task<IEnumerable<Student>> GetAll();
    Task<Student> GetById(int Id);
    Task<Student> CreateStudent(Student student);
    Student DeleteStudent(Student student);
    Student UpdateStudent(Student student);
}