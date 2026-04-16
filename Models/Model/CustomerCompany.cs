namespace Models;

public class CustomerCompany
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Cvr { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyPhoneNumber { get; set; }
    public string CompanyEmail { get; set; }
    public List<Product> FavoriteProducts { get; set; } = new();

    // Forbindelse til DiscountAgreement
    public List<DiscountAgreement> DiscountAgreements { get; set; } = new();
    
    
}