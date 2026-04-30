using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<UserDto?> CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
    Task<UserDto?> LoginAsync(string brugernavn, string password);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetValidResetTokenAsync(string token);
    Task<User?> GetByResetTokenAsync(string token);
    
    Task<bool> ToggleFavoriteAsync(Guid userId, Guid productId);
    Task<IEnumerable<ProductDto>> GetFavoritesAsync(Guid userId);
}

public class UserRepository : IUserRepository
{
    private readonly LajmiContext _context;

    public UserRepository(LajmiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _context.User
            .Include(u => u.CustomerCompany)
            .ToListAsync();

        return users.Select(u => u.ToDto());
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _context.User
            .Include(u => u.CustomerCompany)
            .FirstOrDefaultAsync(u => u.UserId == id);

        return user?.ToDto();
    }

    public async Task<UserDto?> CreateAsync(User user)
    {
        if (user.UserId == Guid.Empty) user.UserId = Guid.NewGuid();

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        // Reload for at få Company med
        return await GetByIdAsync(user.UserId);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null) return false;

        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

// metode til log-in page
    public async Task<UserDto?> LoginAsync(string brugernavn, string password)
    {
        //Hent bruger med samme brugernavn fra DB
        var user = await _context.User
            .Include(u => u.CustomerCompany)
            .FirstOrDefaultAsync(u =>
                u.UserName == brugernavn
            );
        //Fandt vi brugeren?
        if (user == null)
        {
            return null;
        }

        //Password check
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return user.ToDto();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.User
            .FirstOrDefaultAsync(u => u.UserEmail == email);
    }

    public async Task<User?> GetValidResetTokenAsync(string token)
    {
        return await _context.User
            .FirstOrDefaultAsync(u =>
                u.PasswordResetToken == token &&
                u.PasswordResetTokenExpiry > DateTime.UtcNow);
    }

    public async Task<User?> GetByResetTokenAsync(string token)
    {
        return await _context.User
            .FirstOrDefaultAsync(u => u.PasswordResetToken == token);
    }

    // toggle favorite produkt
    public async Task<bool> ToggleFavoriteAsync(Guid userId, Guid productId)
    {
        var user = await _context.User.Include(u => u.FavoriteProducts).FirstOrDefaultAsync(u => u.UserId == userId);
       
        var product = await _context.Product.FindAsync(productId);

        if (user == null || product == null) return false;

        if (user.FavoriteProducts.Any(p => p.ProductId == productId))
        {
            user.FavoriteProducts.Remove(product);
        }
        else
        {
            user.FavoriteProducts.Add(product);
        }

        await _context.SaveChangesAsync();
        return true;
    }
    
    //get all favorites
    public async Task<IEnumerable<ProductDto>> GetFavoritesAsync(Guid userId)
    {
        var user = await _context.User.Include(u => u.FavoriteProducts).FirstOrDefaultAsync(u => u.UserId == userId);
        return user?.FavoriteProducts.Select(p => p.ToDto()) ?? new List<ProductDto>();
    }
}