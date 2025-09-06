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

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int id)
   {
      var student = await _studentService.GetById(id);
      
      return Ok(student);
   }

   [HttpPost]
   public async Task<IActionResult> Create(Student student)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      var newStudent = new Student
      {
         Id = student.Id,
         FirstName = student.FirstName,
         LastName = student.LastName,
         Email = student.Email,
         Phone = student.Phone,
         Address = student.Address,
         BirthDate = student.BirthDate,
         GPA = student.GPA,
         EnrollmentDate = student.EnrollmentDate,
         DepartmentId = student.DepartmentId
      };
      await _studentService.CreateStudent(newStudent);
      return Ok(newStudent);
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> Update(Student student, int id)
   {
      if (!ModelState.IsValid)
         return BadRequest(ModelState);
      var studentFind = await _studentService.GetById(id);
      
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

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
      var studentFind = await _studentService.GetById(id);
      
      _studentService.DeleteStudent(studentFind);

      return Ok(studentFind);
   }
}