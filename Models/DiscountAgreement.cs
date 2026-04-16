namespace Models;

public class DiscountAgreement
{
    public Guid DiscountId { get; set; }
    public decimal DicountPercentage { get; set; }
    public string AgreementDescription { get; set; }
}