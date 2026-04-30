using DataAcces.Context;
using DataAcces.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Test;

public class UserRepositoryTests : IDisposable
{
    private readonly LajmiContext context;
    private readonly IUserRepository repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LajmiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        context = new LajmiContext(options);
        repository = new UserRepository(context);

        UdfyldDatabase();
    }

    private void UdfyldDatabase()
    {
        var companyId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        context.CustomerCompany.Add(new CustomerCompany
        {
            CompanyId = companyId,
            CompanyName = "Test Company",
            Cvr = "12345678",
            CompanyAddress = "Test Address",
            CompanyPhoneNumber = "12345678",
            CompanyEmail = "test@company.com"
        });

        context.User.AddRange(new List<User>
        {
            new User
            {
                UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserName = "user1",
                UserEmail = "user1@example.com",
                UserPhoneNumber = "11111111",
                Password = "hash1",
                CompanyId = companyId
            },
            new User
            {
                UserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserName = "user2",
                UserEmail = "user2@example.com",
                UserPhoneNumber = "22222222",
                Password = "hash2",
                CompanyId = companyId
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
        var userId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        // Act
        var result = await repository.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user1", result.UserName);
    }

    [Fact]
    public async Task CreateAsyncTest()
    {
        // Arrange
        var newUser = new User
        {
            UserName = "newuser",
            UserEmail = "newuser@example.com",
            UserPhoneNumber = "33333333",
            Password = "newhash",
            CompanyId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var result = await repository.CreateAsync(newUser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("newuser", result.UserName);
        Assert.Equal(3, (await repository.GetAllAsync()).Count());
    }

    [Fact]
    public async Task UpdateAsyncTest()
    {
        // Arrange
        var userId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var user = await context.User.FindAsync(userId);
        user.UserName = "updateduser";

        // Act
        var result = await repository.UpdateAsync(user);

        // Assert
        Assert.True(result);
        var updatedUser = await context.User.FindAsync(userId);
        Assert.Equal("updateduser", updatedUser.UserName);
    }

    [Fact]
    public async Task DeleteAsyncTest()
    {
        // Arrange
        var userId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        // Act
        var result = await repository.DeleteAsync(userId);

        // Assert
        Assert.True(result);
        Assert.Null(await context.User.FindAsync(userId));
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
