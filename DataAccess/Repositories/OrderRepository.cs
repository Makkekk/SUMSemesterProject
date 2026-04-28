using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetAllActiveOrdersAsync();
    Task<IEnumerable<OrderDto>> GetOrdersByCompanyIdAsync(Guid companyId);
    Task<OrderDto> CreateOrder(CreateOrderRequest order);
    Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
}

public class OrderRepository : IOrderRepository
{
    private readonly LajmiContext _context;
    
    public OrderRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllActiveOrdersAsync()
    {
        var orders = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .Where(o => o.OrderStatus == OrderStatus.ACTIVE)
            .ToListAsync(); 

        return orders.Select(o => OrderMapper.MapToDto(o));
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCompanyIdAsync(Guid companyId)
    {
        var orders = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .Where(o => o.CompanyId == companyId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return orders.Select(o => OrderMapper.MapToDto(o));
    }

    public async Task<OrderDto> CreateOrder(CreateOrderRequest request)
    {
        var newOrder = new Order
        {
            OrderId = Guid.NewGuid(),
            CompanyId = request.CompanyId,
            OrderLines = request.Lines.Select(l =>
                {
                    var product = _context.Product.Find(l.ProductId);
                    return new OrderLine
                    {
                        OrderLineId = Guid.NewGuid(),
                        ProductId = l.ProductId,
                        ProductName = product?.ProductName ?? "Ukendt",
                        OrderQuantity = l.Quantity,
                        UnitPrice = product?.ProductPrice ?? 0
                    };
                }).ToList()
        };
        
        _context.Order.Add(newOrder);
        await _context.SaveChangesAsync();

        var gemt = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .FirstAsync(o => o.OrderId == newOrder.OrderId);

        return OrderMapper.MapToDto(gemt);
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        var order = OrderMapper.MapToEntity(orderDto);

        await _context.Order.AddAsync(order);
        await _context.SaveChangesAsync();

        var saved = await _context.Order
            .Include(o => o.CustomerCompany)
            .Include(o => o.OrderLines)
            .FirstAsync(o => o.OrderId == order.OrderId);

        return OrderMapper.MapToDto(saved);
    }
}
