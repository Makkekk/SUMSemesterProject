using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Context;


public class LajmiContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<DiscountAgreement> DiscountAgreement { get; set; }
    public DbSet<CustomerCompany> CustomerCompany { get; set; }
    //public DbSet<OrderLine> OrderLine { get; set; } måske ikke nødvendig, da den bliver oprettet via ordren
    //public DbSet<OrderLabel> orderLabel { get; set; } Forstiller mig det er det samme som ovenstående
    //public DbSet<DiscountAgreement> DiscountAgreement { get; set;} måske man skal lave en query for alle virksomheder
    //public DbSet<FavoriteProduct> FavoriteProduct { get; set; } er det bare en query på en mange til mange relation

    public LajmiContext(DbContextOptions<LajmiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Comapny -> Users (1:N)
        modelBuilder.Entity<CustomerCompany>().HasMany(c => c.Users).WithOne(u => u.CustomerCompany).HasForeignKey(u => u.CompanyId);

        //Company -> Orders (1:N)
        modelBuilder.Entity<CustomerCompany>().HasMany(c => c.Orders).WithOne(o => o.CustomerCompany).HasForeignKey(o => o.CompanyId);

        //Order -> OrderLines (Komposition)
        modelBuilder.Entity<Order>().HasMany(o => o.OrderLines).WithOne().HasForeignKey(ol => ol.OrderId).OnDelete(DeleteBehavior.Cascade); //Hvis orderne er slettet, slettes ordlinjen

        //Orderline -> Product (N:1)
        modelBuilder.Entity<OrderLine>().HasOne<Product>().WithMany().HasForeignKey(ol => ol.ProductId);

        //Order -> Orderlabels (1:N)
        modelBuilder.Entity<Order>().HasMany(o => o.OrderLabels).WithOne(ol => ol.Order).HasForeignKey(ol => ol.OrderId);

        //Company -> DiscountAgreement (1:1)
        modelBuilder.Entity<CustomerCompany>().HasOne(c => c.DiscountAgreement).WithOne(d => d.CustomerCompany).HasForeignKey<DiscountAgreement>(d => d.CompanyId);


        //map enumklassen OrderStatus til string istedet for 0-1-2-3, Enumen er gemt istedet for ints
        modelBuilder.Entity<Order>().Property(o => o.OrderStatus).HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}