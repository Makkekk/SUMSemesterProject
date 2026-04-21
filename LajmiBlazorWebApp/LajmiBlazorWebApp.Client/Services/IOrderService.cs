using DTO;

namespace LajmiBlazorWebApp.Client.Services;

interface IOrderService
{
    Task<List<OrderDto>> GetAllActiveProducts();
     
}
