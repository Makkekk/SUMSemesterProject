using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetAllActiveProducts();

    Task<OrderDto> CreateOrder(CreateOrderRequest order);
}

public class OrderRepository :  IOrderRepository
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
    
    
    // Opret Ordre methode.... fuck den her metode. Jeg tror aldrig jeg har haft så svært ved at sætte et objekt sammen som dette... 
    // seriøst det her design sutter max røv

    public async Task<OrderDto> CreateOrder(CreateOrderRequest request)
    {
        var newOrder = new Order
        {
            OrderId =  Guid.NewGuid(),
            CompanyId = request.CompanyId,
            OrderLines = request.Lines.Select(l =>
                {
                    var product = _context.Product.Find(l.ProduceId);
                    return new OrderLine
                    {
                        OrderLineId = Guid.NewGuid(),
                        ProductId = l.ProduceId,
                        ProductName = product?.ProductName ?? "Ukendt",
                        OrderQuantity = l.Quantity,
                        UnitPrice = product?.ProductPrice ?? 0
                    };
                }).ToList()
        };
        
        _context.Order.Add(newOrder);
        
        await _context.SaveChangesAsync();
        // Og jeg kan af en eller anden virkelig skod grund ikke kalde MapToDto før at jeg har hentet objektet fra DB IGEN! 
        var gemt = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .FirstAsync(o => o.OrderId == newOrder.OrderId);

        return OrderMapper.MapToDto(gemt);
    }
}