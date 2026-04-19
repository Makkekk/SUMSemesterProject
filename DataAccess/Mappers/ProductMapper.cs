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
            Id = product.Id,
            Name = product.ProductName,
            Description = product.ProductDescription,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        };
    }
}