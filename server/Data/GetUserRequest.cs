using System.ComponentModel.DataAnnotations;

namespace server.Data;

public class GetUserRequest : IValidatableObject
{
    
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string email { get; set; }
    
    public string userid { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(userid))
        {
            yield return new ValidationResult("Either email or userid must be provided", new[] { nameof(email), nameof(userid) });
        }
    }
}