namespace DTO;

public class CustomerCompanyDto
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Cvr { get; set; }
    public string Adress  { get; set; }

    public decimal? CurrentDiscountPercentage { get; set; }
}

public class CreateCompanyRequest
{
    public string CompanyName { get; set; }
    public string Cvr { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyPhoneNumber { get; set; }
    public string CompanyEmail { get; set; }
}