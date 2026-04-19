using LajmiBlazorWebApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace LajmiBlazorWebApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        
        builder.Services.AddScoped<BasketService>();

        builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7174")
            });

        await builder.Build().RunAsync();
    }
}