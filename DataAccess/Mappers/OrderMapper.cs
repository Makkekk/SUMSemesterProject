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
            OrderDate = order.OrderDate,
            status = order.OrderStatus.ToString(),
            // Her antages det at company er inkluderet
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
}