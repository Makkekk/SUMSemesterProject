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
        var orders = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .Where(o => o.OrderStatus == OrderStatus.ACTIVE)
            .ToListAsync();

        return orders.Select(o => OrderMapper.MapToDto(o));
    }


    public async Task<Order?> GetById(Guid id)
    {
        return await _context.Order
            .Include(o => o.OrderLabels)
            .Include(o => o.OrderLines)
            .Include(o => o.CustomerCompany)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task AddOrderLabel(OrderLabel label)
    {
        await _context.Set<OrderLabel>().AddAsync(label);
    }
}