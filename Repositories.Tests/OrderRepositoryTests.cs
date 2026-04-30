using DataAcces.Context;
using DataAcces.Repositories;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Test;

public class OrderRepositoryTests : IDisposable
{
    private readonly LajmiContext context;
    private readonly IOrderRepository repository;

    public OrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LajmiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        context = new LajmiContext(options);
        repository = new OrderRepository(context);

        UdfyldDatabase();
    }

    private void UdfyldDatabase()
    {
        var companyId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var productId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        context.CustomerCompany.Add(new CustomerCompany
        {
            CompanyId = companyId,
            CompanyName = "Test Company",
            Cvr = "12345678",
            CompanyAddress = "Test Address",
            CompanyPhoneNumber = "12345678",
            CompanyEmail = "test@company.com"
        });

        context.Product.Add(new Product
        {
            ProductId = productId,
            ProductName = "Test Product",
            ProductDescription = "Test Description",
            ProductPrice = 100,
            ImageUrl = "http://example.com/image.jpg",
            Vat = 25,
            ProductWeight = 10
        });

        context.Order.Add(new Order
        {
            OrderId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            CompanyId = companyId,
            OrderStatus = OrderStatus.ACTIVE,
            OrderDate = DateTime.Now,
            OrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    OrderLineId = Guid.NewGuid(),
                    ProductId = productId,
                    ProductName = "Test Product",
                    OrderQuantity = 1,
                    UnitPrice = 100
                }
            }
        });

        context.SaveChanges();
    }

    [Fact]
    public async Task GetAllActiveOrdersAsyncTest()
    {
        // Act
        var result = await repository.GetAllActiveOrdersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOrdersByCompanyIdAsyncTest()
    {
        // Arrange
        var companyId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Act
        var result = await repository.GetOrdersByCompanyIdAsync(companyId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task CreateOrderTest()
    {
        // Arrange
        var companyId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var productId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var request = new CreateOrderRequest
        {
            CompanyId = companyId,
            Lines = new List<CreateOrderLineRequest>
            {
                new CreateOrderLineRequest
                {
                    ProductId = productId,
                    Quantity = 5
                }
            }
        };

        // Act
        var result = await repository.CreateOrder(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(companyId, result.CompanyId);
        Assert.Single(result.Lines);
        Assert.Equal(5, result.Lines.First().Quantity);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
