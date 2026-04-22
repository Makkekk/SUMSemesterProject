using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllActiveProducts();
     
}
