using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using server.Data;

public class WebscrapeResponse {
    
    [Required (ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required (ErrorMessage = "Url is required")]
    [DataType (DataType.Url)]
    public string Url { get; set; }
    
    [Required (ErrorMessage = "Type is required")]
    public string Type { get; set; }
    
    [Required (ErrorMessage = "Ingredients are required")]
    public Ingredient[] Ingredients { get; set; }
    
    [Required (ErrorMessage = "Instructions are required")]
    public string[] Instructions { get; set; }
    
    [DefaultValue(0)]
    public int Serves { get; set; }
    
    [DefaultValue(0)]
    public int PrepTime { get; set; }
    
    [DefaultValue(0)]
    public int CookTime { get; set; }
    
    [DefaultValue(new string[] {})]
    public string[] Images { get; set; }
}