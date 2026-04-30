using DataAcces.Context;
using DataAcces.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Test;

public class DiscountAgreementRepositoryTests : IDisposable
{
    private readonly LajmiContext context;
    private readonly IDiscountAgreementRepository repository;

    public DiscountAgreementRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LajmiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        context = new LajmiContext(options);
        repository = new DiscountAgreementRepository(context);

        UdfyldDatabase();
    }

    private void UdfyldDatabase()
    {
        context.DiscountAgreement.AddRange(new List<DiscountAgreement>
        {
            new DiscountAgreement
            {
                DiscountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                DiscountPercentage = 10,
                AgreementDescription = "10% Discount",
                CompanyId = Guid.NewGuid()
            },
            new DiscountAgreement
            {
                DiscountId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                DiscountPercentage = 20,
                AgreementDescription = "20% Discount",
                CompanyId = Guid.NewGuid()
            }
        });
        context.SaveChanges();
    }

    [Fact]
    public async Task GetAllDiscountsAsyncTest()
    {
        // Act
        var result = await repository.GetAllDiscountsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, d => d.DiscountPercentage == 10);
        Assert.Contains(result, d => d.DiscountPercentage == 20);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
