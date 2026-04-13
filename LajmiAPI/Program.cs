using MongoDB.Driver;
using DataAcces.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// MongoDB
var mongoSection = builder.Configuration.GetSection("MongoDB");
builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoSection["ConnectionString"]));
builder.Services.AddSingleton<LajmiContext>(sp =>
    new LajmiContext(
        sp.GetRequiredService<IMongoClient>(),
        mongoSection["DatabaseName"]!));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
