namespace Student_Management_API.Services;

public interface IEnrollmentsService
{
    Task<IEnumerable<Enrollment>> GetAll();
    Task<Enrollment> GetById(int id);
    Task<Enrollment> Create(Enrollment enrollment);
    Enrollment Update(Enrollment enrollment);
    Enrollment Delete(Enrollment enrollment);
}