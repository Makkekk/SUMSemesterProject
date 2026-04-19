using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LajmiContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // New .NET 10 way

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Serves the JSON
    app.MapScalarApiReference(); // Serves the UI at /scalar/v1
}

app.UseCors("AllowBlazorApp");

// Test connection
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<LajmiContext>();
        var canConnect = context.Database.CanConnect();
        Console.WriteLine(canConnect ? "✓ Database connection successful!" : "✗ Connection failed");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Connection error: {ex.Message}");
    }
}

app.MapGet("/", () => "API is running");
app.MapControllers();
app.Run();
