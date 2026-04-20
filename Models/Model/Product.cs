using System.ComponentModel.DataAnnotations;

namespace Models;

public class Product
{
    [Key]
    public Guid ProductId { get; set; } // <-- ikke i klassediagrammet
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }

    public bool IsCoffee { get; set; } =
        true; // <-- Ikke i klassediagrammet Tjek på om produktet er kaffe og om den så behøver en liste med vægt.

    public List<int>? ProductQuantity { get; set; } =
        [250, 500, 1000]; // <-- ikke i klassediagrammet. Kaffeprodukter kan være 250g, 500g eller 1000g

    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal Vat { get; set; }
    public double ProductWeight { get; set; }
}