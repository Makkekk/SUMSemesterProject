using DataAcces.Repositories;
using Models;
using DTO;

namespace BusinessLogic.Services;

public class BackendOrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly CompanyRepository _companyRepository;
    private readonly ShipmondoService _shipmondoService;

    public BackendOrderService(OrderRepository orderRepository, CompanyRepository companyRepository, ShipmondoService shipmondoService)
    {
        _orderRepository = orderRepository;
        _companyRepository = companyRepository;
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
    
    public async Task CreateOrderFromShopify(ShopifyOrderDto dto)
    {
        var existing = await _orderRepository.GetByShopifyId(dto.Id);
        if (existing != null)
            return;
        
        var company = await _companyRepository.GetCompanyByEmail(dto.Email);

        // 3. Hvis den ikke findes → opret
        if (company == null)
        {
            company = new CustomerCompany
            {
                CompanyId = Guid.NewGuid(),
                CompanyEmail = dto.Email,
                CompanyName = dto.Email // midlertidigt navn
            };

            await _companyRepository.AddCompany(company);
        }
        
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            ShopifyOrderId = dto.Id,
            OrderStatus = OrderStatus.ACTIVE,
            CompanyId = company.CompanyId,
            OrderLines = dto.LineItems.Select(li => new OrderLine
            {
                ProductName = li.Title,
                OrderQuantity = li.Quantity
            }).ToList()
        };

        await _orderRepository.Add(order);
        await _orderRepository.SaveChanges();
    }
    
}