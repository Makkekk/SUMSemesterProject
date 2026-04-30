using LajmiBlazorWebApp.Client.Services;
using Models;
using Xunit;

namespace LajmiBlazorWebApp.Client.Tests;

public class CalculateDiscountServiceTests
{
    [Fact]
    public void CalculatePriceTest()
    {
        // Arrange
        var service = new CalculateDiscountService();
        var product = new Product { ProductPrice = 500m };
        decimal discount = 0.15m; 

        // Act
        var result = service.CalculatePrice(product, discount);

        // Assert
        // 500 * (1 - 0.15) = 500 * 0.85 = 425
        Assert.Equal(425m, result);
    }

    [Fact]
    public void CalculatePriceWithZeroDiscountTest()
    {
        // Arrange
        var service = new CalculateDiscountService();
        var product = new Product { ProductPrice = 500m };
        decimal discount = 0m;

        // Act
        var result = service.CalculatePrice(product, discount);

        // Assert
        Assert.Equal(500m, result);
    }
}
