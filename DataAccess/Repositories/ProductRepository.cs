using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid id);
    Task<ProductDto> GetProductByNameAsync(string name);
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task<ProductDto> UpdateProductAsync(ProductDto productDto);
}

public class ProductRepository : IProductRepository
{
    private readonly LajmiContext _context;

    public ProductRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Product.ToListAsync();
        return products.Select(p => p.ToDto());
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _context.Product.FindAsync(id);
        return product?.ToDto();
    }

    public async Task<ProductDto> GetProductByNameAsync(string name)
    {
        var product = await _context.Product.FirstOrDefaultAsync(p => p.ProductName == name);
        return product?.ToDto();
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        var product = productDto.ToEntity();
        
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
        
        return product.ToDto();
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
    {
        var existingProduct = await _context.Product.FindAsync(productDto.ProductId);
        if (existingProduct == null) return null;

        existingProduct.ProductName = productDto.ProductName;
        existingProduct.ProductDescription = productDto.Description;
        existingProduct.ProductPrice = productDto.ProductPrice;
        existingProduct.ImageUrl = productDto.ImageUrl;
        existingProduct.Vat = productDto.Vat;
        existingProduct.ProductWeight = productDto.ProductWeight;

        await _context.SaveChangesAsync();
        return existingProduct.ToDto();
    }
}
