using System.ComponentModel.DataAnnotations;

namespace DTO;

public class ForgotPasswordDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";
}
