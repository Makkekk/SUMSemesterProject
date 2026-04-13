using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace DataAcees.Context;


public class LajmiContext : DbContext {
    bool created = Database.EnsureCreated();
        if (created) {
        Debug.WriteLine("Database created");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    }

}