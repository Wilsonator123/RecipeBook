using MongoDB.Driver;
using server.Data;

namespace server.database;

public class Mongo
{
    private static readonly Lazy<MongoClient> LazyClient = 
        new Lazy<MongoClient>(() => new MongoClient("mongodb://root:Bulstrode_52@localhost:27017"));
    
    public static MongoClient Client => LazyClient.Value;
    
    private readonly IMongoDatabase _database;

    
    public Mongo(string databaseName)
    {
        _database = Client.GetDatabase(databaseName);
    }
  
    public IMongoCollection<RecipeItem> Recipes => _database.GetCollection<RecipeItem>("recipes");
}