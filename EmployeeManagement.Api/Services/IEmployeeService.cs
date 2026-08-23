using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Services;

public interface IEmployeeService
{
    Task<Employee> CreateEmployeeAsync(CreateEmployeeDto dto);
    // US-02 Additions
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(string id);
}