using System.Diagnostics.CodeAnalysis;
using DTO;
using Models;

namespace DataAcces.Mappers;

public class OrderMapper
{
    public static OrderDto MapToDto(Order order)
    {
        if (order == null) return null;

        return new OrderDto
        {
            orderId = order.OrderId,
            CompanyId = order.CompanyId,
            OrderDate = order.OrderDate,
            status = order.OrderStatus.ToString(),
            CompanyName = order.CustomerCompany?.CompanyName ?? "Ukendt",
            Lines = order.OrderLines?.Select(ol => OrderMapper.ToDto(ol)).ToList() ?? new()
        };
    }

    public static OrderlineDto ToDto(OrderLine orderLine)
    {
        if (orderLine == null)
        {
            return null;
        }

        return new OrderlineDto
        {
            ProductId = orderLine.ProductId,
            ProductName = orderLine.ProductName,
            Quantity = orderLine.OrderQuantity,
            UnitPrice = orderLine.UnitPrice
        };
    }

    public static Order MapToEntity(OrderDto dto)
    {
        if (dto == null) return null;

        return new Order
        {
            OrderId = Guid.NewGuid(),
            CompanyId = dto.CompanyId,
            OrderDate = DateTime.UtcNow,
            OrderStatus = OrderStatus.ACTIVE,
            OrderLines = dto.Lines?.Select(line => new OrderLine
            {
                OrderLineId = Guid.NewGuid(),
                ProductId = line.ProductId,
                ProductName = line.ProductName,
                OrderQuantity = line.Quantity,
                UnitPrice = line.UnitPrice
            }).ToList() ?? new()
        };
    }
}