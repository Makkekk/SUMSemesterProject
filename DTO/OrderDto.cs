namespace DTO;

public class OrderDto
{
    public Guid orderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string status { get; set; }
    public string CompanyName { get; set; }
    
    public List<OrderlineDto> Lines { get; set; }

    public decimal TotalAmount => Lines.Sum(l => l.SubTotal);
}

public class OrderlineDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal => Quantity * UnitPrice;
}