using DataAcces.Context;
using DataAcces.Repositories;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Test;

public class ProductRepositoryTests : IDisposable
{
    private readonly LajmiContext context;
    private readonly IProductRepository repository;
    
    public ProductRepositoryTests() {
        // 1. Opsæt InMemory Database
        var options = new DbContextOptionsBuilder<LajmiContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        
        context = new LajmiContext(options);
        repository = new ProductRepository(context);
        
        UdfyldDatabase();
    }

    private void UdfyldDatabase()
    {
        context.Product.AddRange(new List<Product>
        {
            new Product
            {
                ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                ProductName = "Test 1",
                ProductDescription = "Beskrivelse 1",
                ProductPrice = 100,
                ImageUrl = "http://example.com/product1.jpg",
                Vat = 25, 
                ProductWeight = 20
            },
            new Product()
            {
                ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"), 
                ProductName = "Test 2",
                ProductDescription = "Beskrivelse 2",
                ProductPrice = 20,
                ImageUrl = "http://example.com/product2.jpg", 
                Vat = 25,
                ProductWeight = 30
            },
        });
        context.SaveChanges();
    }
    
    [Fact]
    public async Task GetAllProductsAsyncTest()
    {
        //Act
        var result = await repository.GetAllProductsAsync();
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public async Task GetProductByIdAsyncTest()
    {
        //Arrange
        var productId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        
        //Act
        var result = await repository.GetProductByIdAsync(productId);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal("Test 1", result.ProductName);
    }

    [Fact]
    public async Task CreateProductAsyncTest()
    {
        //Arrange
        var newProductDto = new ProductDto
        {
            ProductName = "Test 3",
            Description = "Beskrivelse 3",
            ProductPrice = 50,
            ImageUrl = "http://example.com/product3.jpg",
            Vat = 25,
            ProductWeight = 10
        };
        
        //Act
        var result = await repository.CreateProductAsync(newProductDto);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal("Test 3", result.ProductName);
        Assert.Equal(3, (await repository.GetAllProductsAsync()).Count());
        
        var dbProduct = await context.Product.FindAsync(result.ProductId);
        Assert.Equal("Test 3",dbProduct.ProductName);
    }
    
    public void Dispose()
    {
        context.Dispose();
    }
}