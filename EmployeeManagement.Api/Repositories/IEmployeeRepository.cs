using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> CreateAsync(Employee employee);
    Task<Employee?> GetByEmailAsync(string email);
}