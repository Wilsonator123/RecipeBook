namespace server.utils;

public class Response
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public Response(bool status, string message, object data = null)
    {
        Status = status;
        Message = message;
        Data = data;
    }
}