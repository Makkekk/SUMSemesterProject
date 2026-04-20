using DTO;
using Models;

namespace DataAcces.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product product)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.ProductName,
            Description = product.ProductDescription,
            Price = product.ProductPrice,
            ImageUrl = product.ImageUrl
        };
    }
}