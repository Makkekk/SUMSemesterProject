using LajmiBlazorWebApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace LajmiBlazorWebApp.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        
        builder.Services.AddScoped<BasketService>();

        await builder.Build().RunAsync();
    }
}