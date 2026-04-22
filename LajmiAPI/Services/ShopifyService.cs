using DTO;
using ShopifySharp;


namespace LajmiAPI.Services;

public class ShopifyService
{
    private readonly string shopUrl;
    private readonly string accessToken;

    public ShopifyService(IConfiguration configuration)
    {
        shopUrl = configuration["Shopify:ShopUrl"] ?? "";
        accessToken = configuration["Shopify:AccessToken"] ?? "";
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
}