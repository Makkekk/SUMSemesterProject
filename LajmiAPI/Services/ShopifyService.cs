using DataAcces.Repositories;
using DTO;
using ShopifySharp;


namespace LajmiAPI.Services;

public class ShopifyService
{
    private readonly string shopUrl;
    private readonly string accessToken;
    private readonly IProductRepository _productRepository;

    public ShopifyService(IConfiguration configuration, IProductRepository productRepository)
    {
        shopUrl = configuration["Shopify:ShopUrl"] ?? "";
        accessToken = configuration["Shopify:AccessToken"] ?? "";
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> GetShopifyProductAsync()
    {
        var productService = new ProductService(shopUrl, accessToken);
        var shopifyProducts = await productService.ListAsync();

        return shopifyProducts.Items.Select(p => new ProductDto()
        {
            ProductId = Guid.NewGuid(),
            Name = p.Title,
            Description = p.BodyHtml ?? "",
            Price = p.Variants.FirstOrDefault()?.Price ?? 0,
            ImageUrl = p.Images.FirstOrDefault()?.Src ?? "",
            Vat = 0.25m,
            ProductWeight = (double)(p.Variants.FirstOrDefault()?.Weight ?? 0)
        }).ToList();
    }

    public async Task<int> SyncProductsDatabase()
    {
        var shopifyProducts = await GetShopifyProductAsync();
        
        var existingProducts = await _productRepository.GetAllProductsAsync();
        int importCount = 0;

        foreach (var shopifyProduct in shopifyProducts)
        {
            if (!existingProducts.Any(p => p.Name == shopifyProduct.Name))
            {
                await _productRepository.CreateProductAsync(shopifyProduct);
                importCount++;
            }
        }
        return importCount;
        
    }
}