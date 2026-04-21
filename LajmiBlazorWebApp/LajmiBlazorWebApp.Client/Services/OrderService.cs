using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class OrderService : IOrderService
{
    public Task<List<ProductDto>> GetAllActiveProducts()
    {
        HttpClient client = new HttpClient();
        
        client.GetAsync("https://localhost:5001/api/Order/GetAllActiveProducts"); // ehhh.... hjæælp mig
        
        throw new NotImplementedException();
    }
}