using System.ComponentModel.DataAnnotations;

namespace Models;

public class CustomerCompany
{
    [Key]
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    [Required]

    public string Cvr { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyPhoneNumber { get; set; }
    public string CompanyEmail { get; set; }

    public List<Product> FavoriteProducts { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<User> Users { get; set; } = new();

    // Forbindelse til DiscountAgreement
    public DiscountAgreement DiscountAgreement { get; set; } = new();
}

