using DataAcces.Context;
using DataAcces.Repositories;
using LajmiAPI.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LajmiContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDiscountAgreementRepository, DiscountAgreementRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<BackendOrderService>();
builder.Services.AddScoped<ShipmondoService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ShopifyService>();


builder.Services.AddControllers();
builder.Services.AddOpenApi(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowBlazorApp");


using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<LajmiContext>();
        DataInitializer.Initialize(context);
        Console.WriteLine("Database initialized and seeded successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
    }
}

app.MapGet("/", () => "API is running");
app.MapControllers();
app.Run();
