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
    
    // Denne metode henter alle ordre fra databasen som har "ACTIVE" som 
    // Kig i LajmiContext for at se konvertering af enum til string og omvendt.... jeg hader enums
    public async Task<IEnumerable<OrderDto>> GetAllActiveOrdersFromDb()
    {
        var orders = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .Where(o => o.OrderStatus == OrderStatus.ACTIVE).ToListAsync();
        
        return orders.Select(o => OrderMapper.MapToDto(o));
    }
}