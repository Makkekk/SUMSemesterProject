namespace LajmiBlazorWebApp.Client.Services;
using Models;

public class CalculateDiscountService
{
    public decimal CalculatePrice(Product product, decimal discount)
    {
        // m sikre at talet læses som decimal
        return product.ProductPrice * (1m - discount);
    }
}