using DataAcces.Context;
using DataAcces.Mappers;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(Guid id);
    Task<UserDto> CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
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

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var user = await _context.User
            .Include(u => u.CustomerCompany)
            .FirstOrDefaultAsync(u => u.UserId == id);

        return user?.ToDto();
    }

    public async Task<UserDto> CreateAsync(User user)
    {
        if (user.UserId == Guid.Empty) user.UserId = Guid.NewGuid();

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
        if (user.PasswordHash != password)
            {
                return null;
            }
        return user.ToDto();
        
    }
}
