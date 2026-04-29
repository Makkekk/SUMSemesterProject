using System.ComponentModel.DataAnnotations;

namespace Models;

public class OrderLabel
{
    [Key]
    public Guid OrderLabelId { get; set; }

    public string TrackingNumber { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
}