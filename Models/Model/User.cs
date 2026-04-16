namespace Models;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string? UserPhoneNumber { get; set; }

    // Relation til CustomerCompany
    public Guid CompanyId { get; set; }
    public CustomerCompany Company { get; set; }
    
}