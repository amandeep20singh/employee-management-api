using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    /// <summary>
    /// US-01: Create a new employee record
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdEmployee = await _service.CreateEmployeeAsync(dto);
            return CreatedAtAction(nameof(Create), new { id = createdEmployee.Id }, createdEmployee);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>
    /// US-02: Get all employees
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _service.GetAllEmployeesAsync();
        return Ok(employees);
    }

    /// <summary>
    /// US-02: Get employee by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound(new { message = $"Employee with ID '{id}' was not found." });
        }

        return Ok(employee);
    }
}