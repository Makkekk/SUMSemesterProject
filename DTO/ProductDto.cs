namespace DTO;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal ProductPrice { get; set; }
    public string ImageUrl { get; set; }
    public decimal Vat { get; set; }
    public double ProductWeight { get; set; }
}
