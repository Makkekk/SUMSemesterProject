using ShopifySharp;

namespace LajmiAPI.Services;

public class ShopifySyncService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly TimeSpan interval = TimeSpan.FromHours(1);
    
    public ShopifyService(IServiceProvider serviceProvider)
    {
        serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var shopifyService = scope.ServiceProvider.GetRequiredService<IShopifyService>();
                await shopifyService.SyncProductsDatabase();
            }
            await Task.Delay(interval, stoppingToken);
        }
    }

}