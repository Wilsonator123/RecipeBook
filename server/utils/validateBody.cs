using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace server.utils;


public static class ValidateBody
{
    
    public static async Task<(bool, T)> Validate<T>(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        
        if (string.IsNullOrWhiteSpace(body))
        {
            return (false, default);
        }
        
        var userData = JsonSerializer.Deserialize<T>(body);
        
        if (userData == null)
        {
            return (false, default);
        }
        
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(userData);
        if (!Validator.TryValidateObject(userData, validationContext, validationResults, true))
        {
            var errors = validationResults.Select(vr => vr.ErrorMessage).ToArray();
            return (false, default);
        }

        return (true, userData);
    }
}