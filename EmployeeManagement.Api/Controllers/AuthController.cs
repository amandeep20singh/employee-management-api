using EmployeeManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Simple demo credentials check
        if (request.Username == "admin" && request.Password == "password123")
        {
            var token = _authService.GenerateJwtToken(request.Username, "Admin");
            return Ok(new { token });
        }

        return Unauthorized(new { message = "Invalid username or password" });
    }
}

public record LoginRequest(string Username, string Password);