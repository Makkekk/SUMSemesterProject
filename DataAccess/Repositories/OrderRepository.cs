using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public class OrderRepository
{
    private readonly LajmiContext _context;
    
    public OrderRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllActiveProducts()
    {
        return await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .Where( o => o.OrderStatus == OrderStatus.ACTIVE)
            .Select(o => OrderMapper.MapToDto(o))
            .ToListAsync();
    }
}