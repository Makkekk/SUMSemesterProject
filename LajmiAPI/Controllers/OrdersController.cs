using DataAcces.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLogic.Services;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    //opret ny ordre, og find ordrehistorik
    private readonly LajmiContext _context; // < -- Burde den være her ? Jeg går udfra at vi bruger Repository metoderne og ikke rækker direkte ned i DB ?
    private readonly OrderRepository _orderRepository;
    private readonly BackendOrderService _backendOrderService;

    public OrdersController(OrderRepository orderRepository, BackendOrderService backendOrderService)
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        _backendOrderService = backendOrderService;
    }
    
    [HttpGet("activeOrders")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllActiveOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllActiveOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("approve/{orderId}")]
    public async Task<IActionResult> ApproveOrder(Guid orderId)
    {
        await _backendOrderService.ApproveOrder(orderId);
        return Ok();
    }
    
}

    [HttpGet("company/{companyId}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCompany(Guid companyId)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersByCompanyIdAsync(companyId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var order = await _orderRepository.CreateOrder(request);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
