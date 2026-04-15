using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Context;

public class LajmiContext : DbContext {
    public LajmiContext(DbContextOptions<LajmiContext> options) : base(options) {
        
    }
    
}