using DTO;

namespace LajmiBlazorWebApp.Client.Services;
using Models;

public class CalculateDiscountService
{
    // Dette skal reduceres til et kald til logikken som skal ligge i BIL (businessLogic projektet) 
    public decimal CalculatePrice(ProductDto productDto, decimal discount)
    {
        // m sikre at talet læses som decimal
        return productDto.ProductPrice * (1m - discount);
    }
}