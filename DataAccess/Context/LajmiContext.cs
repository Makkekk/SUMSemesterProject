using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Context;

public class LajmiContext : DbContext {
    public DbSet<User> Customer { get; set; }
    
    public LajmiContext(DbContextOptions<LajmiContext> options) : base(options) {
        
    }
    
}