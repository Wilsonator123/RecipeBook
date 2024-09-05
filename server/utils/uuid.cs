using server.database;

namespace server.utils;

public class GUID
{
    
    public static async Task<string> GenerateGUID()
    {
        do {
            Guid guid = Guid.NewGuid();
            
            List<Object[]> response = await Database.Query("isUUIDtaken", guid.ToString());
            
            if (response.Count == 0)
            {
                return guid.ToString();
            }
        } while (true);
    }
}