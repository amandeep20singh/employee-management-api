namespace EmployeeManagement.Api.Services;

public interface IAuthService
{
    string GenerateJwtToken(string username, string role);
}