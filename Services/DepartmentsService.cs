using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Student_Management_API.Services;

public class DepartmentsService : IDepartmentsService
{
    private readonly ApplicationDbContext _context;

    public DepartmentsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAll()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department> GetById(int id)
    {
        return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Department> CreateDepartment(Department department)
    {
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
        
        return department;
    }

    public Department UpdateDeparment(Department department)
    {
        _context.Departments.Update(department);
        _context.SaveChanges();

        return department;
    }

    public Department DeleteDepartment(Department department)
    {
        _context.Departments.Remove(department);
        _context.SaveChanges();

        return department;
    }
}