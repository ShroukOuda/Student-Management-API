using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_API.Filters;
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
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _coursesService.GetAll();

        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _coursesService.GetById(id);

        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newCourse = new Course
        {
            Id = course.Id,
            Name = course.Name,
            Code = course.Code,
            Credits = course.Credits,
            Description = course.Description,
            InstructorName = course.InstructorName,
            MaxEnrollment = course.MaxEnrollment,
            DepartmentId = course.DepartmentId 
        };
        await _coursesService.CreateCourse(newCourse);
        return Ok(newCourse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Course course, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var courseFind = await _coursesService.GetById(id);

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _coursesService.GetById(id);

        _coursesService.DeleteCourse(course);
        return Ok(course);
    }
}