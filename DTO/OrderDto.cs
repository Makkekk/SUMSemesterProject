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