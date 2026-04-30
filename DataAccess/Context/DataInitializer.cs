using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAcces.Context;

public static class DataInitializer
{
    public static void Initialize(LajmiContext context)
    {
        try
        {
            Console.WriteLine("Applying migrations...");

            context.Database.Migrate();

            var adminUser = context.User.FirstOrDefault(u => u.UserName == "admin");
            if (adminUser == null)
            {
                Console.WriteLine("Seeding admin user...");
                var adminCompanyId = Guid.NewGuid();
                context.CustomerCompany.Add(new CustomerCompany
                {
                    CompanyId = adminCompanyId,
                    CompanyName = "Lajmi Admin",
                    Cvr = "00000000",
                    CompanyAddress = "Adminvej 1",
                    CompanyPhoneNumber = "+45 0000 0000",
                    CompanyEmail = "admin@lajmi.dk"
                });
                context.User.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "admin",
                    UserEmail = "admin@lajmi.dk",
                    UserPhoneNumber = "+45 0000 0000",
                    Password = "admin",
                    IsAdmin = true,
                    CompanyId = adminCompanyId
                });

                // Add a regular test user
                context.User.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "testuser",
                    UserEmail = "test@lajmi.dk",
                    UserPhoneNumber = "+45 1234 5678",
                    Password = "password",
                    IsAdmin = false,
                    CompanyId = adminCompanyId
                });
                context.SaveChanges();
            }
            else if (!adminUser.IsAdmin)
            {
                adminUser.IsAdmin = true;
                context.SaveChanges();
            }

            if (context.Product.Any())
            {
                return; // Already seeded
            }

            Console.WriteLine("Seeding dummy data...");
            SeedData(context);

            context.SaveChanges();
            Console.WriteLine("Database migration and seeding completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during Initialize: {ex.Message}");
            if (ex.InnerException != null) Console.WriteLine($"Inner: {ex.InnerException.Message}");
            throw;
        }
    }

    private static void SeedData(LajmiContext context)
    {
        // --- Products ---
        var products = new List<Product>
        {
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Brazil - Mantiqueira Azul (250g)",
                ProductDescription = "MANTIQUEIRA AZUL er en af vores absolutte bestsellere. Balanceret og behagelig kop med noter af marcipan, chokolade, nødder og røde bær.",
                ImageUrl = "https://lajmi.dk/cdn/shop/files/azul.jpg?v=1776258588&width=1024",
                Vat = 0.25m,
                ProductWeight = 250,
                ProductPrice = 85.00m
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Brazil - Mantiqueira RUBY (250g)",
                ProductDescription = "MANTIQUEIRA RUBY er vores kraftigste og mest fyldige kaffe. Fyldig og rund kop med noter af mørk chokolade, karamel og kakao.",
                ImageUrl = "https://lajmi.dk/cdn/shop/files/Ruby.jpg?",
                Vat = 0.25m,
                ProductWeight = 250,
                ProductPrice = 80.00m
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Brazil - MANTIQUEIRA AZUL (500g)",
                ProductDescription = "Større pose af vores populære Mantiqueira Azul.",
                ImageUrl = "https://lajmi.dk/cdn/shop/files/azul.jpg?v=1776258588&width=1024",
                Vat = 0.25m,
                ProductWeight = 500,
                ProductPrice = 145.00m
            }
        };
        context.Product.AddRange(products);

        // --- Companies ---
        var company1 = new CustomerCompany
        {
            CompanyId = Guid.NewGuid(),
            CompanyName = "JamalsPalace",
            Cvr = "12345678",
            CompanyAddress = "Kebabvej 1, 8000 Aarhus",
            CompanyPhoneNumber = "+45 1234 5678",
            CompanyEmail = "contact@JamalsPalace.com",
            DiscountAgreement = new DiscountAgreement
            {
                DiscountId = Guid.NewGuid(),
                DiscountPercentage = 10.0m,
                AgreementDescription = "Standard kebab rabat",
                DiscountValidFrom = DateTime.UtcNow,
                DiscountValidTo = DateTime.UtcNow.AddYears(1)
            }
        };

        var company2 = new CustomerCompany
        {
            CompanyId = Guid.NewGuid(),
            CompanyName = "NikkeLajElskerKaffe",
            Cvr = "87654321",
            CompanyAddress = "Kaffegaden 15, 2100 København",
            CompanyPhoneNumber = "+45 8765 4321",
            CompanyEmail = "info@NikkeLajElskerKaffe.dk",
            DiscountAgreement = new DiscountAgreement
            {
                DiscountId = Guid.NewGuid(),
                DiscountPercentage = 15.0m,
                AgreementDescription = "Sustainability partnership",
                DiscountValidFrom = DateTime.UtcNow,
                DiscountValidTo = DateTime.UtcNow.AddMonths(6)
            }
        };
        context.CustomerCompany.AddRange(company1, company2);

        // --- Users ---
        var users = new List<User>
        {
            new User
            {
                UserId = Guid.NewGuid(),
                UserName = "Jamal Alaoui",
                UserEmail = "jamal@JamalsPalace.com",
                UserPhoneNumber = "+45 1122 3344",
                CompanyId = company1.CompanyId
            },
            new User
            {
                UserId = Guid.NewGuid(),
                UserName = "Nikkelaj Nikkelaj",
                UserEmail = "nikkelaj@NikkeLajElskerKaffe.dk",
                UserPhoneNumber = "+45 5566 7788",
                CompanyId = company2.CompanyId
            }
        };
        context.User.AddRange(users);

        // --- Orders ---
        var order1 = new Order
        {
            OrderId = Guid.NewGuid(),
            CompanyId = company1.CompanyId,
            OrderDate = DateTime.UtcNow.AddDays(-2),
            OrderStatus = OrderStatus.DONE,
            OrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    OrderLineId = Guid.NewGuid(),
                    ProductId = products[1].ProductId,
                    ProductName = products[1].ProductName,
                    OrderQuantity = 5,
                    UnitPrice = products[1].ProductPrice
                }
            },
            OrderLabels = new List<OrderLabel>
            {
                new OrderLabel
                {
                    OrderLabelId = Guid.NewGuid(),
                    Carrier = "PostNord",
                    TrackingNumber = "PN123456789DK"
                }
            }
        };

        var order2 = new Order
        {
            OrderId = Guid.NewGuid(),
            CompanyId = company2.CompanyId,
            OrderDate = DateTime.UtcNow.AddDays(-1),
            OrderStatus = OrderStatus.IN_PROGRESS,
            OrderLines = new List<OrderLine>
            {
                new OrderLine
                {
                    OrderLineId = Guid.NewGuid(),
                    ProductId = products[0].ProductId,
                    ProductName = products[0].ProductName,
                    OrderQuantity = 10,
                    UnitPrice = products[0].ProductPrice
                },
                new OrderLine
                {
                    OrderLineId = Guid.NewGuid(),
                    ProductId = products[2].ProductId,
                    ProductName = products[2].ProductName,
                    OrderQuantity = 2,
                    UnitPrice = products[2].ProductPrice
                }
            }
        };

        context.Order.AddRange(order1, order2);
    }
}
