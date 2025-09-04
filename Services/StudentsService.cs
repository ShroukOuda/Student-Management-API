using Microsoft.EntityFrameworkCore;

namespace Student_Management_API.Services;

public class StudentsService : IStudentsService
{
    private readonly ApplicationDbContext _context;

    public StudentsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        var students = await _context
            .Students
            .ToListAsync();
        return students;
    }

    public async Task<Student> GetById(int Id)
    {
        var student = await _context
            .Students
            .SingleOrDefaultAsync(s => s.Id == Id);
        return student;
    }

    public async Task<Student> CreateStudent(Student student)
    {
        await _context.AddAsync(student);
        await _context.SaveChangesAsync();
        
        return student;
    }

    public Student DeleteStudent(Student student)
    {
        _context.Remove(student);
        _context.SaveChanges();

        return student;
    }

    public Student UpdateStudent(Student student)
    { 
        _context.Update(student);
        _context.SaveChanges();

        return student;
    }
}