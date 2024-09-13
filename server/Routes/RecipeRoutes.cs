using System.ComponentModel.DataAnnotations;
using server.utils;
using server.recipe;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using server.Data;


namespace server.Routes;


public static class RecipeRoutes
{
    public static IApplicationBuilder UseRecipeRoutes(this IApplicationBuilder builder)
    {
        return builder.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/recipe/getRecipes", async (context) =>
            {
                var cookie = CookieHelper.GetUserIdFromCookies(context);

                string userId = Auth.ValidateToken(cookie);

                Response response = await Recipe.GetRecipes(userId);

                if (response.Status)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    return;
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("No recipes found");
                    return;
                }
            });

            endpoints.MapGet("/recipe/getRecipe/{recipeid}", async context =>
            {
                var cookie = CookieHelper.GetUserIdFromCookies(context);

                string userId = Auth.ValidateToken(cookie);
                
                
                var recipeId = (string)context.Request.RouteValues["recipeid"];
                
                if (string.IsNullOrWhiteSpace(recipeId))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid recipe id");
                    return;
                }
                
                Response response = await Recipe.GetRecipe(userId, recipeId);

                if (response.Status)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    return;
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("No recipes found");
                    return;
                }


            });


            endpoints.MapPost("/recipe/createRecipe", async context =>
            {

                var cookie = CookieHelper.GetUserIdFromCookies(context);

                string userId = Auth.ValidateToken(cookie);

                var (isValid, userData) = await ValidateBody.Validate<CreateRecipeRequest>(context);

                if (!isValid)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid request body");
                    return;
                }

                Response response = await Recipe.CreateRecipe(userId, userData);

                if (response.Status)
                {
                    Console.WriteLine(response.Message);
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    return;
                }
                else
                {
                    Console.WriteLine(response.Message);
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("No recipes found");
                    return;
                }
            });

            endpoints.MapPost("/recipe/webscrape", async context =>
            {
                var cookie = CookieHelper.GetUserIdFromCookies(context);

                string userId = Auth.ValidateToken(cookie);

                var (isValid, userData) = await ValidateBody.Validate<WebscrapeRequest>(context);

                if (!isValid)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid request body");
                    return;
                }

                WebscrapeResponse response = await WebScrape.GetRecipe(userData.Url);
                
                if (response == null)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    return;
                }

                CreateRecipeRequest recipe = new CreateRecipeRequest()
                {
                    Name = response.Name,
                    Url = response.Url,
                    Type = response.Type,
                    Ingredients = response.Ingredients,
                    Instructions = response.Instructions,
                    PrepTime = response.PrepTime,
                    CookTime = response.CookTime,
                    Serves = response.Serves,
                    Images = response.Images
                };
                
                Response createResponse = await Recipe.CreateRecipe(userId, recipe);

                
                if (createResponse.Status)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(createResponse));
                    return;
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("No recipes found");
                    return;
                }
            });
        });

        // endpoints.MapPost("/recipe/updateRecipe", async context =>
        // {
        //
        // })

        // endpoints.MapPost("/recipe/deleteRecipe", async context =>
        // {
        //
        // })
    }
}
