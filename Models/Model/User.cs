using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string UserEmail { get; set; }
    [Required]
    public string? UserPhoneNumber { get; set; }
    
    public string Password { get; set; } 
    
    public bool IsAdmin { get; set; }

    // Relation til CustomerCompany
    public Guid CompanyId { get; set; }

    public CustomerCompany CustomerCompany { get; set; }
    
    public string? PasswordResetToken { get; set; }
    
    public DateTime? PasswordResetTokenExpiry { get; set; }
}