using DataAcces.Repositories;
using Models;

namespace BusinessLogic.Services;

public class BackendOrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly ShipmondoService _shipmondoService;

    public BackendOrderService(OrderRepository orderRepository, ShipmondoService shipmondoService)
    {
        _orderRepository = orderRepository;
        _shipmondoService = shipmondoService;
    }

    public async Task ApproveOrder(Guid orderId)
    {
        var order = await _orderRepository.GetById(orderId);

        if (order == null)
            throw new Exception("Order not found");

        order.OrderStatus = OrderStatus.APPROVED;

        var label = await _shipmondoService.CreateShipment(order);

        label.OrderId = order.OrderId;

        await _orderRepository.AddOrderLabel(label);

        await _orderRepository.SaveChanges();
    }
}