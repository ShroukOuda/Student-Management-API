namespace Student_Management_API.Services;

public interface IDepartmentsService
{
    Task<IEnumerable<Department>> GetAll();
    Task<Department> GetById(int Id);
    Task<Department> CreateDepartment(Department department);
    Department UpdateDeparment(Department department);
    Department DeleteDepartment(Department department);
    
}