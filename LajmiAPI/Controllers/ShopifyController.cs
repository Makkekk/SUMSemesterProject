using DTO;
using LajmiAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopifyController : ControllerBase
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

    [HttpPost("sync")]
    public async Task<ActionResult<string>> SyncProducts()
    {
        var count = await _service.SyncProductsDatabase();
        return Ok($"{count} nye produkter blev synkroniseret til databasen!");
    }
}