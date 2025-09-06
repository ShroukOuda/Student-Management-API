using Microsoft.EntityFrameworkCore;

namespace Student_Management_API.Services;

public class EnrollmentsService : IEnrollmentsService
{
    private readonly ApplicationDbContext _context;

    public EnrollmentsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enrollment>> GetAll()
    {
        return await _context.Enrollments.ToListAsync();
    }

    public async Task<Enrollment> GetById(int id)
    {
        return await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Enrollment> Create(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();

        return enrollment;
    }

    public Enrollment Update(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        _context.SaveChanges();

        return enrollment;
    }

    public Enrollment Delete(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();

        return enrollment;
    }
}