using System.ComponentModel.DataAnnotations;
namespace Models;

public class Product
{
    [Key]
    public Guid ProductId { get; set; } // <-- ikke i klassediagrammet
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ImageUrl { get; set; }
    public decimal Vat { get; set; }
    public double ProductWeight { get; set; }
    public decimal ProductPrice { get; set; }
}