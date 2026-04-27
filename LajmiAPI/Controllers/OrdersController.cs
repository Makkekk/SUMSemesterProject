using DataAcces.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
