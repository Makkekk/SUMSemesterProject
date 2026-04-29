using System.Net.Http.Json;
using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    
    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<OrderDto>> GetAllActiveOrdersAsync()
    {
        try
        {
            var orders = await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/Orders/activeOrders");
            return orders ?? new List<OrderDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching active orders: {ex.Message}");
            return new List<OrderDto>();
        }
    }

    public async Task<List<OrderDto>> GetOrdersByCompanyIdAsync(Guid companyId)
    {
        try
        {
            var orders = await _httpClient.GetFromJsonAsync<List<OrderDto>>(
                $"api/Orders/company/{companyId}");
            return orders ?? new List<OrderDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching order history: {ex.Message}");
            return new List<OrderDto>();
        }
    }

    public async Task<OrderDto?> CreateOrder(CreateOrderRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Orders", request);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<OrderDto>();
    }
}
