namespace DTO;

public class CreateOrderRequest
{
    public Guid CompanyId { get; set; }
    public List<CreateOrderLineRequest> Lines { get; set; } = new();
}
