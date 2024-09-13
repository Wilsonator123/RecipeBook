using MongoDB.Driver;
using server.Data;
using MongoDB.Driver.GridFS;

namespace server.database;

public class Mongo
{
    private static readonly Lazy<MongoClient> LazyClient = 
        new Lazy<MongoClient>(() => new MongoClient("mongodb://dbUser:Bulstrode_52@localhost:27017"));
    
    public static MongoClient Client => LazyClient.Value;
    
    private readonly IMongoDatabase _database;
    private readonly GridFSBucket _gridFs;
    
    public Mongo(string databaseName)
    {
        _database = Client.GetDatabase(databaseName);
        _gridFs = new GridFSBucket(_database);
    }
  
    public IMongoCollection<RecipeItem> Recipes => _database.GetCollection<RecipeItem>("recipes");
    
    public GridFSBucket GridFS => _gridFs;
}