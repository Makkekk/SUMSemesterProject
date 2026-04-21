using DataAcces.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace LajmiAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly LajmiContext _context;

    public DiscountController(LajmiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<DiscountAgreement>>> GetAllDiscountsAsync()
    {
        return await _context.DiscountAgreement.ToListAsync();
    }
}