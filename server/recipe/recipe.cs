using MongoDB.Driver;
using server.Data;
using server.database;
using server.utils;

namespace server.recipe;

public static class Recipe
{
    private static readonly Mongo Mongo = new("RecipeBook");
    
    public static async Task<Response> GetRecipes(string userid)
    {
        var recipes = await Mongo.Recipes.Find(r => r.UserId == userid).ToListAsync();
        return new Response(true, "Recipes found", recipes);
    }
    
    public static async Task<Response> GetRecipe(string userid, string id)
    {
        var recipe = await Mongo.Recipes.Find(r => r.Id == id && r.UserId == userid).FirstOrDefaultAsync();
        return recipe != null ? new Response(true, "Recipe found", recipe) : new Response(false, "Recipe not found");
    }
    
    public static async Task<Response> CreateRecipe(RecipeItem recipe)
    {
        await Mongo.Recipes.InsertOneAsync(recipe);
        return new Response(true, "Recipe created", recipe);
    }
    
    public static async Task<Response> UpdateRecipe(RecipeItem recipe)
    {
        var result = await Mongo.Recipes.ReplaceOneAsync(r => r.Id == recipe.Id, recipe);
        return result.IsAcknowledged ? new Response(true, "Recipe updated", recipe) : new Response(false, "Recipe not updated");
    }
    
    public static async Task<Response> DeleteRecipe(string id)
    {
        var result = await Mongo.Recipes.DeleteOneAsync(r => r.Id == id);
        return result.IsAcknowledged ? new Response(true, "Recipe deleted") : new Response(false, "Recipe not deleted");
    }
    
    
    
    // public static async Task test()
    // {
    //     var newRecipe = new RecipeItem
    //     {
    //         Name = "Chocolate Cake",
    //         CreatedAt = DateTime.UtcNow,
    //         UserId = "fd71dfca-6703-4be8-9dea-5b8ba533db6b",
    //         Url = "http://example.com/chocolate-cake",
    //         Type = SourceType.website.ToString(),
    //         Ingredients = new Ingredient[]
    //         {
    //             new Ingredient { Name = "Flour", Quantity = 2, Unit = "cups" },
    //             new Ingredient { Name = "Sugar", Quantity = 1, Unit = "cup" }
    //         },
    //         Rating = 5,
    //         Tags = new string[] { "dessert", "chocolate" },
    //         Instructions = new string[] { "Mix ingredients", "Bake at 350F for 30 minutes" }
    //     };
    //
    //     await Mongo.Recipes.InsertOneAsync(newRecipe);
    //
    //     var recipes = await Mongo.GetRecipes();
    //     foreach (var recipe in recipes)
    //     {
    //         Console.WriteLine($"Recipe: {recipe.Name}, Created At: {recipe.CreatedAt}");
    //     }
    // }
}