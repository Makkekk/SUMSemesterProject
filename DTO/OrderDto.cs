using Models;

namespace DTO;

public class OrderDto
{
    public Guid orderId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime OrderDate { get; set; }
    public string status { get; set; }
    public string CompanyName { get; set; }
    
    public List<OrderlineDto> Lines { get; set; } = new();

    public decimal TotalAmount => Lines.Sum(l => l.SubTotal);
}


// Hallo !! Burde Orderline ikke have sin egen klasse, som så denne klasse kender til ? Det her nested shit er next level
public class OrderlineDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal => Quantity * UnitPrice;
}

public class CreateOrderRequest
{
public Guid CompanyId { get; set; }
public List<CreateOrderLineRequest> Lines { get; set; } = new();

}

public class CreateOrderLineRequest
{
    public Guid ProduceId { get; set; }
    public int Quantity { get; set; }
}