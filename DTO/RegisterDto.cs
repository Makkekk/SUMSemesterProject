using System.ComponentModel.DataAnnotations;

namespace DTO;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string UserEmail { get; set; }
    [Required]
    public string? UserPhoneNumber { get; set; }
    [Required]
    public string Password { get; set; } 
}