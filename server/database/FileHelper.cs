using Newtonsoft.Json;

namespace server.database;

public class FileHelper
{
    public static Dictionary<string, Query>? ReadJson()
    {
        using (StreamReader r = new StreamReader("database/queries.json"))
        {
            string json = r.ReadToEnd();
            Dictionary<string, Query>? queries = JsonConvert.DeserializeObject<Dictionary<string, Query>>(json);
            return queries;
        }

    }

    public static Query GetQuery(string query)
    {
        var queriesList = ReadJson();

        Query? statement = null;

        queriesList.TryGetValue(query , out statement);

        if (statement is null){
            return new Query("Query Not Found", 0);
        }
        return statement;
    }
}

public class Query(string query, int noParams){
    public readonly string query = query;
    public int noParams = noParams;

    public override string ToString()
    {
        return $"""
                Query = {query}
                Number of Paramas = {noParams}
                """;
    }
}