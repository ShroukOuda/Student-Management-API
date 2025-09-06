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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await _departmentsService.GetById(id);

        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Department department)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newDepartment = new Department
        {
            Id = department.Id,
            Name = department.Name,
            ManagerName = department.ManagerName
        };
        await _departmentsService.CreateDepartment(newDepartment);

        return Ok(newDepartment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Department department, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var departmentFind = await _departmentsService.GetById(id);
        
        departmentFind.Name = department.Name;
        departmentFind.ManagerName = department.ManagerName;
        _departmentsService.UpdateDeparment(departmentFind);

        return Ok(departmentFind);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var departmentFind = await _departmentsService.GetById(id);


        _departmentsService.DeleteDepartment(departmentFind);
        return Ok(departmentFind);
    }
}