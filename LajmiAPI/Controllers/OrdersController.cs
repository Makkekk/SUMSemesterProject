using DataAcces.Context;
using Microsoft.AspNetCore.Mvc;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    //opret ny ordre, og find ordrehistorik
    private readonly LajmiContext _context;
}