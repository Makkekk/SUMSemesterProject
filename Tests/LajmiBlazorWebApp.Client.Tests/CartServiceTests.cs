using LajmiBlazorWebApp.Client.Services;
using Models;
using Xunit;

namespace LajmiBlazorWebApp.Client.Tests;

public class CartServiceTests
{
    [Fact]
    public void AddProductcounterTest()
    {
        // Arrange
        var cartService = new CartService();

        // Act
        cartService.AddProductcounter();

        // Assert
        Assert.Equal(1, cartService.NumOfProducts);
    }

    [Fact]
    public void AddProductTest()
    {
        // Arrange
        var cartService = new CartService();
        var product = new Product { ProductName = "Test Product", ProductPrice = 100m };

        // Act
        cartService.AddProduct(product);

        // Assert
        Assert.Single(cartService.Items);
        Assert.Equal(product, cartService.Items[0]);
    }

    [Fact]
    public void NotifyStateChangedTest()
    {
        // Arrange
        var cartService = new CartService();
        bool eventTriggered = false;
        cartService.OnChange += () => eventTriggered = true;

        // Act
        cartService.NotifyStateChanged();

        // Assert
        Assert.True(eventTriggered);
    }

    [Fact]
    public void AddProductTriggersEvenTest()
    {
        // Arrange
        var cartService = new CartService();
        bool eventTriggered = false;
        cartService.OnChange += () => eventTriggered = true;
        var product = new Product { ProductName = "Test Product", ProductPrice = 100m };

        // Act
        cartService.AddProduct(product);

        // Assert
        Assert.True(eventTriggered);
    }

    [Fact]
    public void GetDiscountedPriceTest()
    {
        // Arrange
        var cartService = new CartService();
        var product = new Product { ProductPrice = 100m };
        decimal discount = 0.2m; // 20% discount

        // Act
        var result = cartService.GetDiscountedPrice(product, discount);

        // Assert
        Assert.Equal(80m, result);
    }

    [Fact]
    public void GetTotalPriceTest()
    {
        // Arrange
        var cartService = new CartService();
        cartService.AddProduct(new Product { ProductPrice = 100m });
        cartService.AddProduct(new Product { ProductPrice = 200m });
        decimal discount = 0.1m; // 10% discount

        // Act
        var result = cartService.GetTotalPrice(discount);

        // Assert
        // (100 * 0.9) + (200 * 0.9) = 90 + 180 = 270
        Assert.Equal(270m, result);
    }
}
