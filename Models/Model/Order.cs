using System.ComponentModel.DataAnnotations;

namespace Models;

public class Order
{

    [Key]
    public Guid OrderId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }

    // Forbindelse til CustomerCompany
    public CustomerCompany CustomerCompany { get; set; }
    
    // Composition til Ordeline
    public List<OrderLine> OrderLines { get; set; } = new();

    // Forbindelse til OrderLabels
    public List<OrderLabel> OrderLabels { get; set; } = new();
}