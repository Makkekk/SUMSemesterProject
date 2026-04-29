using System.ComponentModel.DataAnnotations;
namespace Models;

public class DiscountAgreement
{
    [Key]
    public Guid DiscountId { get; set; }
    public decimal DiscountPercentage { get; set; }
    public string AgreementDescription { get; set; }

    public DateTime DiscountValidFrom { get; set; }
    public DateTime DiscountValidTo { get; set; }

    // Foreign key til CustomerCompany
    // Denne bruges af EF til at koble DiscountAgreement til en virksomhed
    public Guid CompanyId { get; set; }
    
    // Navigation tilbage til CustomerCompany
    // Gør relationen tydelig for EF (så den IKKE laver egne FK-navne)
    // "= null!" bruges fordi EF sætter værdien – ikke os manuelt
    public CustomerCompany CustomerCompany { get; set; } = null!;
    
}
}
