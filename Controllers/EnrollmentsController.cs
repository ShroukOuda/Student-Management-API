using Microsoft.AspNetCore.Mvc;
using Student_Management_API.Services;

namespace Student_Management_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentsService _enrollmentsService;

    public EnrollmentsController(IEnrollmentsService enrollmentsService)
    {
        _enrollmentsService = enrollmentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _enrollmentsService.GetAll();
        
        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var enrollment = await _enrollmentsService.GetById(id);

        return Ok(enrollment);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Enrollment enrollment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var newEnrollment = new Enrollment
        {
            Id = enrollment.Id,
            EnrollmentDate = enrollment.EnrollmentDate,
            Grade = enrollment.Grade,
            EnrollmentStatus = enrollment.EnrollmentStatus,
            CourseId = enrollment.CourseId,
            StudentId = enrollment.StudentId
        };

        await _enrollmentsService.Create(newEnrollment);
        return Ok(newEnrollment);
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Enrollment enrollment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var enrollmentFind = await _enrollmentsService.GetById(id);

        enrollmentFind.Id = enrollment.Id;
        enrollmentFind.EnrollmentDate = enrollment.EnrollmentDate;
        enrollmentFind.Grade = enrollment.Grade;
        enrollmentFind.EnrollmentStatus = enrollment.EnrollmentStatus;
        enrollmentFind.CourseId = enrollment.CourseId;
        enrollmentFind.StudentId = enrollment.StudentId;

        _enrollmentsService.Update(enrollmentFind);

        return Ok(enrollmentFind);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var enrollmentFind = await _enrollmentsService.GetById(id);

        _enrollmentsService.Delete(enrollmentFind);

        return Ok(enrollmentFind);
    }
}