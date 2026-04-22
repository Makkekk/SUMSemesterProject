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
            ImageUrl = product.ImageUrl,
            Vat = product.Vat,
            ProductWeight = product.ProductWeight
        };
    }

    public static Product ToEntity(this ProductDto productDto)
    {
        if (productDto == null)
            return null;

        return new Product
        {
            ProductId = productDto.ProductId == Guid.Empty ? Guid.NewGuid() : productDto.ProductId,
            ProductName = productDto.Name,
            ProductDescription = productDto.Description,
            ProductPrice = productDto.Price,
            ImageUrl = productDto.ImageUrl,
            Vat = productDto.Vat,
            ProductWeight = productDto.ProductWeight
        };
    }
}
