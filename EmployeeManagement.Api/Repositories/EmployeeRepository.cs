using EmployeeManagement.Api.Models;
using MongoDB.Driver;

namespace EmployeeManagement.Api.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _employees;

    public EmployeeRepository(IMongoDatabase database)
    {
        _employees = database.GetCollection<Employee>("Employees");

        // Ensure unique index on Email per technical specification in US-01
        var indexKeys = Builders<Employee>.IndexKeys.Ascending(e => e.Email);
        var indexOptions = new CreateIndexOptions { Unique = true };
        _employees.Indexes.CreateOne(new CreateIndexModel<Employee>(indexKeys, indexOptions));
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await _employees.Find(e => e.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        await _employees.InsertOneAsync(employee);
        return employee;
    }
}
