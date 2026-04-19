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

    public Guid CompanyId { get; set; }
    public CustomerCompany CustomerCompany { get; set; } 
}