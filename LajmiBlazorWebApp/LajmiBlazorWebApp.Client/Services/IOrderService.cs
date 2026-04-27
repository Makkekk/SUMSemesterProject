using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllActiveOrdersAsync();
    Task<List<OrderDto>> GetOrdersByCompanyIdAsync(Guid companyId);
    Task<OrderDto?> CreateOrder(CreateOrderRequest request);
}
