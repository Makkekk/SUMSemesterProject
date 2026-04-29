using System.ComponentModel.DataAnnotations;

namespace Models;

public class CustomerCompany
{
    [Key] public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    [Required] public string Cvr { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyPhoneNumber { get; set; }
    public string CompanyEmail { get; set; }

    public List<Product> FavoriteProducts { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<User> Users { get; set; } = new();

    // Forbindelse til DiscountAgreement
    // Navigation property til DiscountAgreement
    // Gøres nullable, fordi en virksomhed KAN eksistere uden en rabataftale
    // Vi fjerner "= new()" fordi EF selv styrer relationer – ellers tror EF altid der findes en relation
    public DiscountAgreement? DiscountAgreement { get; set; }
}
    public DiscountAgreement? DiscountAgreement { get; set; }
}

