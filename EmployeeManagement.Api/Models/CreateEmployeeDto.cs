using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Api.Models;

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required.")]
    public string Department { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public decimal Salary { get; set; }
}