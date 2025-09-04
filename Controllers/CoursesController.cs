using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_API.Models;
using Student_Management_API.Services;

namespace Student_Management_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _coursesService.GetAll();

        return Ok(courses);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        var course = await _coursesService.GetById(Id);

        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _coursesService.CreateCourse(course);
        return Ok(course);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(Course course, int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var courseFind = await _coursesService.GetById(Id);
        if (courseFind == null)
            return NotFound();

        courseFind.Name = course.Name;
        courseFind.Code = course.Code;
        courseFind.Credits = course.Credits;
        courseFind.Description = course.Description;
        courseFind.InstructorName = course.InstructorName;
        courseFind.MaxEnrollment = course.MaxEnrollment;
        courseFind.DepartmentId = course.DepartmentId;

        _coursesService.UpdateCourse(courseFind);
        return Ok(courseFind);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var course = await _coursesService.GetById(Id);
        if (course == null)
            return NotFound();
        _coursesService.DeleteCourse(course);
        return Ok(course);
    }
}