using System.ComponentModel.DataAnnotations;

namespace server.Data
{
    public class GetRecipeRequest
    {
        [Required (ErrorMessage = "Please enter a valid recipe id")]
        public string id { get; set; }
    }
}
