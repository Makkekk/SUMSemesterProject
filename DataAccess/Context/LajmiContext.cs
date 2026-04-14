using MongoDB.Driver;
using Models;

namespace DataAcces.Context;

public class LajmiContext
{
    private readonly IMongoDatabase _database;

    public LajmiContext(IMongoClient client, string databaseName)
    {
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Company> Companies =>
        _database.GetCollection<Company>("companies");
}
