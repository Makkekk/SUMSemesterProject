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
                    UserName = "test",
                    UserEmail = "test@lajmi.dk",
                    UserPhoneNumber = "+45 1234 5678",
                    Password = "test",
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
            //SeedData(context);

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
}
