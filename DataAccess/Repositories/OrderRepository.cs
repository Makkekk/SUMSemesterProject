using DataAcces.Context;
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
            .Where(o => o.OrderStatus == OrderStatus.ACTIVE)
            .Select(o => new OrderDto
            {
                orderId = o.OrderId,
                OrderDate = o.OrderDate,
                status = o.OrderStatus.ToString(),
                CompanyName = o.CustomerCompany.CompanyName,
                Lines = o.OrderLines.Select(ol => new OrderLineDto){
                ProductId = ol.ProductId,
                Quantity = ol.Quantity
            }).ToList()
            })
            .ToListAsync();

    }
}