namespace DTO;

public class UserDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string? UserPhoneNumber { get; set; }
    
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
}
