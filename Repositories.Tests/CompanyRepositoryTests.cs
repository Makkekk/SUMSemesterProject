using DataAcces.Context;
using DataAcces.Repositories;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Test;

public class CompanyRepositoryTests : IDisposable
{
    private readonly LajmiContext context;
    private readonly ICompanyRepository repository;

    public CompanyRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LajmiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        context = new LajmiContext(options);
        repository = new CompanyRepository(context);

        UdfyldDatabase();
    }

    private void UdfyldDatabase()
    {
        context.CustomerCompany.AddRange(new List<CustomerCompany>
        {
            new CustomerCompany
            {
                CompanyId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CompanyName = "Test Company 1",
                Cvr = "12345678",
                CompanyAddress = "Address 1",
                CompanyPhoneNumber = "11223344",
                CompanyEmail = "test1@example.com"
            },
            new CustomerCompany
            {
                CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                CompanyName = "Test Company 2",
                Cvr = "87654321",
                CompanyAddress = "Address 2",
                CompanyPhoneNumber = "44332211",
                CompanyEmail = "test2@example.com"
            }
        });
        context.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsyncTest()
    {
        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsyncTest()
    {
        // Arrange
        var companyId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Act
        var result = await repository.GetByIdAsync(companyId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Company 1", result.CompanyName);
    }

    [Fact]
    public async Task CreateAsyncTest()
    {
        // Arrange
        var request = new CreateCompanyRequest
        {
            CompanyName = "New Company",
            Cvr = "11223344",
            CompanyAddress = "New Address",
            CompanyPhoneNumber = "55667788",
            CompanyEmail = "new@example.com"
        };

        // Act
        var result = await repository.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Company", result.CompanyName);
        Assert.Equal(3, (await repository.GetAllAsync()).Count());

        var dbCompany = await context.CustomerCompany.FindAsync(result.CompanyId);
        Assert.NotNull(dbCompany);
        Assert.Equal("New Company", dbCompany.CompanyName);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
