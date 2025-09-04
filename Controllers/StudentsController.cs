using Microsoft.AspNetCore.Mvc;
using Student_Management_API.Services;

namespace Student_Management_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
  
   private readonly IStudentsService _studentService;

   public StudentsController(IStudentsService studentService)
   {
      _studentService = studentService;
   }

   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var students = await _studentService.GetAll();

      return Ok(students);
   }

   [HttpGet("{Id}")]
   public async Task<IActionResult> GetById(int Id)
   {
      var student = await _studentService.GetById(Id);

      if (student == null)
      {
         return NotFound($"No Student Found with ID: {Id}");
      }
      return Ok(student);
   }

   [HttpPost]
   public async Task<IActionResult> Create(Student student)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      await _studentService.CreateStudent(student);
      return Ok(student);
   }

   [HttpPut("{Id}")]
   public async Task<IActionResult> Update(Student student, int Id)
   {
      if (!ModelState.IsValid)
         return BadRequest(ModelState);
      var studentFind = await _studentService.GetById(Id);

      if (studentFind == null)
         return NotFound();
      studentFind.FirstName = student.FirstName;
      studentFind.LastName = student.LastName;
      studentFind.Address = student.Address;
      studentFind.BirthDate = student.BirthDate;
      studentFind.Email = student.Email;
      studentFind.EnrollmentDate = student.EnrollmentDate;
      studentFind.GPA = student.GPA;
      studentFind.Phone = student.Phone;
      studentFind.DepartmentId = student.DepartmentId;

      _studentService.UpdateStudent(studentFind);

      return Ok(studentFind);
   }

   [HttpDelete("{Id}")]
   public async Task<IActionResult> Delete(int Id)
   {
      var studentFind = await _studentService.GetById(Id);
      if (studentFind == null)
         return NotFound();
      _studentService.DeleteStudent(studentFind);

      return Ok(studentFind);
   }
}