using DataAcces.Context;
using DataAcces.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    //opret ny ordre, og find ordrehistorik
    private readonly LajmiContext _context; // < -- Burde den være her ? Jeg går udfra at vi bruger Repository metoderne og ikke rækker direkte ned i DB ?
    private readonly OrderRepository _orderRepository;

    public OrdersController(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    [HttpGet("activeOrders")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllActiveOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllActiveOrdersFromDb();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}