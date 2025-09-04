using Microsoft.EntityFrameworkCore;

namespace Student_Management_API.Services;

public class CoursesService : ICoursesService
{
    private readonly ApplicationDbContext _context;

    public CoursesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> GetById(int Id)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<Course> CreateCourse(Course course)
    {
        await _context.Courses.AddAsync(course);
        _context.SaveChangesAsync();
        return course;
    }

    public Course UpdateCourse(Course course)
    {
        _context.Courses.Update(course);
        _context.SaveChangesAsync();
        return course;
    }

    public Course DeleteCourse(Course course)
    {
        _context.Courses.Remove(course);
        _context.SaveChangesAsync();
        return course;
    }
}