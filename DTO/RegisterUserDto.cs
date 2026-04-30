using System.ComponentModel.DataAnnotations;

namespace DTO;

public class RegisterUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string UserEmail { get; set; }
    [Required]
    public string? UserPhoneNumber { get; set; }
    [Required]
    public string Password { get; set; } 
    
    //Company info
    [Required]
    public string Cvr { get; set; }
    
    public string CompanyName { get; set; }
    
    public string CompanyAddress { get; set; }
    
    public string CompanyEmail { get; set; }
    public string CompanyPhoneNumber { get; set; }
    
    
    
}