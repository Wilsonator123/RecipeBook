using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace server.Data;

public class CreateRecipeRequest
{
    [Required (ErrorMessage = "Recipe Name is required")]
    public string Name { get; set; }

    [DefaultValue("none")]
    [DataType(DataType.Url)]
    public string Url { get; set; }
    
    [Required (ErrorMessage = "Type is required")]
    public string Type { get; set; }
    
    [Required (ErrorMessage = "Ingredients are required")]
    public Ingredient[] Ingredients { get; set; }
    
    [DefaultValue(0)]
    public int Rating { get; set; }
    
    [DefaultValue(new String[] {})]
    public string[] Tags { get; set; }
    
    [Required (ErrorMessage = "Instructions are required")]
    public string[] Instructions { get; set; }
}
