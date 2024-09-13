using System.ComponentModel.DataAnnotations;

public class WebscrapeRequest
{
    [Required (ErrorMessage = "Url is required")]
    public string Url { get; set; }
}