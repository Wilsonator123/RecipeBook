using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using server.Data;

namespace server.recipe;

class WebScrape
{
    public static async Task<WebscrapeResponse> GetRecipe(string url)
    {
        try
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var document = new HtmlDocument();
                document.LoadHtml(await response.Content.ReadAsStringAsync());

                var recipe = document.DocumentNode.SelectSingleNode("//div[@class='wprm-recipe-container']");
                List<string> images = new List<string>();
                
                var imgNode = recipe.SelectNodes("//figure[contains(@class, 'block-image')]//img");
                if (imgNode != null)
                {
                    foreach (var img in imgNode)
                    {
                        var src = img.GetAttributeValue("src", string.Empty);
                       
                        if (src != string.Empty && src.StartsWith("http"))
                        {
                            images.Add(src);
                        }
                    }
                }

                if (recipe != null)
                {
                    var (name, prep, cook) = GetRecipeHeader(recipe);
                    
                    var serves = recipe.SelectSingleNode("//span[contains(@class, 'wprm-recipe-servings wprm-recipe-details')]");
                    
                    prep = prep == "" ? "0" : prep;
                    cook = cook == "" ? "0" : cook;
                    var servesText = serves == null ? "0" : serves.InnerText;
                    
                    Ingredient[] ingredients = GetIngredients(recipe);

                    string[] instructions = GetInstructions(recipe);

                    return new WebscrapeResponse()
                    {
                        Name = name,
                        Url = url,
                        Type = "website",
                        Ingredients = ingredients,
                        Instructions = instructions,
                        PrepTime = int.Parse(prep),
                        CookTime = int.Parse(cook),
                        Serves = int.Parse(servesText),
                        Images = images.ToArray()
                    };
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    private static (string, string, string) GetRecipeHeader(HtmlNode recipe)
    {
        try
        {
            var recipeHeader = recipe.SelectSingleNode(".//div[@class='wprm-entry-header']");
            
            HtmlNode name = null;
            HtmlNode cook = null;
            HtmlNode prep = null;
            
            if (recipeHeader == null) {
                name = recipe.SelectSingleNode(".//h2[contains(@class, 'wprm-recipe-name')]");
                cook = recipe.SelectSingleNode(".//span[contains(@class, 'wprm-recipe-cook_time-minutes')]");
                prep = recipe.SelectSingleNode(".//span[contains(@class, 'wprm-recipe-prep_time-minutes')]");
            } else {
                name = recipeHeader.SelectSingleNode(".//h2[contains(@class, 'wprm-recipe-name')]");
                cook = recipeHeader.SelectSingleNode(".//span[contains(@class, 'wprm-recipe-cook_time-minutes')]");
                prep = recipeHeader.SelectSingleNode(".//span[contains(@class, 'wprm-recipe-prep_time-minutes')]");
            }
            
            return (name is null ? "none" : name.InnerText, prep is null ? "0" : prep.FirstChild.InnerText, cook is null ? "0" : cook.FirstChild.InnerText);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default;
        }
    }

    public static Ingredient[] GetIngredients(HtmlNode recipe)
    {
        try{
            List<Ingredient> ingredients = new List<Ingredient>();
            var ingredientGroups = recipe.SelectNodes(".//div[@class='wprm-recipe-ingredient-group']");

            if (ingredientGroups == null) return default;
            foreach (var group in ingredientGroups)
            {   
                var ingredientList = group.SelectNodes(".//ul//li");
                
                foreach (var ingredient in ingredientList)
                {
                    var amount = ingredient.SelectSingleNode(".//span[@class='wprm-recipe-ingredient-amount']");
                    var name = ingredient.SelectSingleNode(".//span[@class='wprm-recipe-ingredient-name']");
                    var unit = ingredient.SelectSingleNode(".//span[@class='wprm-recipe-ingredient-unit']");
                    
                    if (name == null) continue;
                    
                    ingredients.Add(new Ingredient
                    {
                        Name = name.InnerText,
                        Quantity = amount == null ? "none" : amount.InnerText,
                        Unit = unit == null ? "none" : unit.InnerText
                    });
                }
            }
            
            return ingredients.ToArray();
        }
        catch (Exception e)
        {
            return default;
        }
    }

    public static string[] GetInstructions(HtmlNode recipe)
    {
        try
        {
            List<string> instructions = new List<string>();
            var instructionList = recipe.SelectNodes(".//div[@class='wprm-recipe-instruction-group']");

            if (instructionList == null) return default;

            foreach (var instruction in instructionList)
            {
                var steps = instruction.SelectNodes(".//div[@class='wprm-recipe-instruction-text']");

                foreach (var step in steps)
                {
                    instructions.Add(step.InnerText);
                }
            }

            for (int i = 0; i < instructions.Count; i++)
            {
                instructions[i] = instructions[i].Trim();
                instructions[i] = HtmlEntity.DeEntitize(instructions[i]);
            }

            return instructions.ToArray();
        }
        catch (Exception e)
        {
            return default;
        }
    }

}