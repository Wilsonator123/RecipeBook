using System.ComponentModel.DataAnnotations;

namespace server.Data;

public class UserResponse
{
    public string id { get; set; }
    public string email { get; set; }
    public string lname { get; set; }
    public string fname { get; set; }
}