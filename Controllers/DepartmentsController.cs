using Microsoft.AspNetCore.Mvc;
using Student_Management_API.Services;

namespace Student_Management_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentsService _departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService)
    {
        _departmentsService = departmentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _departmentsService.GetAll();

        return Ok(departments);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        var department = await _departmentsService.GetById(Id);

        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Department department)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await _departmentsService.CreateDepartment(department);

        return Ok(department);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(Department department, int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var departmentFind = await _departmentsService.GetById(Id);

        if (departmentFind == null)
            return NotFound();
        
        departmentFind.Name = department.Name;
        departmentFind.ManagerName = department.ManagerName;
        _departmentsService.UpdateDeparment(departmentFind);

        return Ok(departmentFind);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var departmentFind = await _departmentsService.GetById(Id);
        if (departmentFind == null)
            return NotFound();

        _departmentsService.DeleteDepartment(departmentFind);
        return Ok(departmentFind);
    }
}