using System.ComponentModel.DataAnnotations;

namespace Models;

public class OrderLine
{
    [Key]
    public Guid OrderLineId { get; set; }
    public int OrderQuantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Forbindelse til Product (Se det som snapshottet af produkt prisen,
    // således at orderlinjen beholder sin pris, selv hvis produkt prisen blev ændret
    public Guid? ProductId { get; set; }
    public string ProductName { get; set; }

    // Composition til Order
    public Guid OrderId { get; set; }

}