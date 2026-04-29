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
            ProductName = product.ProductName,
            Description = product.ProductDescription,
            ProductPrice = product.ProductPrice,
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
            ProductName = productDto.ProductName,
            ProductDescription = productDto.Description,
            ProductPrice = productDto.ProductPrice,
            ImageUrl = productDto.ImageUrl,
            Vat = productDto.Vat,
            ProductWeight = productDto.ProductWeight
        };
    }
}
