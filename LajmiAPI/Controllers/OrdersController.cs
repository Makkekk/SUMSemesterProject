using DataAcces.Context;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    //opret ny ordre, og find ordrehistorik
    private readonly LajmiContext _context;



    [HttpGet("activeOrders")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllActiveOrders()
    {
        try
        {
            var orders 
        }
    }
}