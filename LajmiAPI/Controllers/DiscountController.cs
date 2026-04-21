using DataAcces.Context;
using DataAcces.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace LajmiAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountAgreementRepository _discountAgreement;

    public DiscountController(IDiscountAgreementRepository discountAgreement)
    {
        _discountAgreement = discountAgreement;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiscountAgreementDto>>> GetAllDiscountsAsync()
    {
        var d = await _discountAgreement.GetAllDiscountsAsync();
            return Ok(d);
    }
}