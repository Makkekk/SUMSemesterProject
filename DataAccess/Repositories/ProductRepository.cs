using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(Guid id);
}

public class ProductRepository : IProductRepository
{
    private readonly LajmiContext _context;

    public ProductRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _context.Product.ToListAsync();
        return products.Select(p => p.ToDto());
    }

    public async Task<ProductDto> GetByIdAsync(Guid id)
    {
        var product = await _context.Product.FindAsync(id);
        return product.ToDto();
    }
}
