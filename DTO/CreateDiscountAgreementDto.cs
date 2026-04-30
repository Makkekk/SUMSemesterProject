namespace DTO;

public class CreateDiscountAgreementDto
{
    public Guid CompanyId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int discountProcentage  { get; set; }
}