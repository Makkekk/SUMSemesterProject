using DataAcces.Context;
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
    {
        _orderRepository = orderRepository;
        _backendOrderService = backendOrderService;
    }
    
    [HttpGet("activeOrders")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllActiveOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllActiveProducts();
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