using LajmiBlazorWebApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace LajmiBlazorWebApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        // Service for at gemme bruger log-in i session
        builder.Services.AddSingleton<UserSessionService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<CartService>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<DiscountAgreementService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<CalculateDiscountService>();
        builder.Services.AddScoped<CompanyService>();
        builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5055")
            });

        await builder.Build().RunAsync();
    }
}