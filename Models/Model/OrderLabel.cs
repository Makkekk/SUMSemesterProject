namespace Models;

public class OrderLabel
{
    public Guid OrderLabelId { get; set; }
    public string TrackingNumber { get; set; }
    public string Carrier { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
}