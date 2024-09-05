using Npgsql;
namespace server.database;

public static class Database
{
    private static readonly NpgsqlConnection Db;
    // public NpgsqlConnection db;

	static Database()
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
	        Host = "localhost,5432",
	        Username = "root",
	        Password = File.ReadAllText("./db/password.txt"),
	        Database = "RecipeBook",
        };

        Db = new NpgsqlConnection(builder.ConnectionString);
    }

    public static async Task<List<Object[]>> Query(string name, params string[] parameters)
    {
	    if (Db.State != System.Data.ConnectionState.Open)
	    {
		    await Db.OpenAsync();
	    }
	    
	    Query statement = FileHelper.GetQuery(name);

        if (statement.query == "Query Not Found" )
        {
            Console.WriteLine("Query Not Found");
            return new List<Object[]>();
        }

		await using var cmd = new NpgsqlCommand(statement.query, Db);
	         
        for ( int i = 0; i < statement.noParams; i++ )
        {
	         cmd.Parameters.AddWithValue($"@{i + 1}", parameters[i]);
	    };
        
	    await using var reader = await cmd.ExecuteReaderAsync();
	     
	    var result = new List<object[]>();

	    while (await reader.ReadAsync())
	    {
		    var row = new object[reader.FieldCount];
		    reader.GetValues(row);
		    result.Add(row);
	    }
	    
	    await Db.CloseAsync();
	    return result;
    }
}