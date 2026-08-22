using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Repositories;

namespace EmployeeManagement.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var existing = await _repository.GetByEmailAsync(dto.Email);
        if (existing != null)
        {
            throw new InvalidOperationException($"Employee with email '{dto.Email}' already exists.");
        }

        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Department = dto.Department,
            Salary = dto.Salary,
            CreatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(employee);
    }
}