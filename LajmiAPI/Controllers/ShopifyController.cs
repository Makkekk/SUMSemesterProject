using DTO;
using LajmiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopifyController
{
    private readonly ShopifyService _service;

    public ShopifyController(ShopifyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProductsAsync()
    {
        var res = await _service.GetShopifyProductAsync();
        return res;
    }
}