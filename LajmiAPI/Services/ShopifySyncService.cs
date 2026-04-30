using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LajmiAPI.Services;

public class ShopifySyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromHours(1);
    
    public ShopifySyncService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Initial delay to let the app start up fully
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var shopifyService = scope.ServiceProvider.GetRequiredService<ShopifyService>();
                    Console.WriteLine($"[{DateTime.Now}] Starting Shopify product sync...");
                    var (imported, updated) = await shopifyService.SyncProductsDatabase();
                    Console.WriteLine($"[{DateTime.Now}] Shopify sync completed. {imported} new products added, {updated} products updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error during Shopify sync: {ex.Message}");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
