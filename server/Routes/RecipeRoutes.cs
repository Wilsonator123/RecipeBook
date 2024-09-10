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

            endpoints.MapGet("/recipe/getRecipe", async context =>
            {
                var cookie = CookieHelper.GetUserIdFromCookies(context);

                string userId = Auth.ValidateToken(cookie);

                var (isValid, userData) = await ValidateBody.Validate<GetRecipeRequest>(context);

                if (!isValid)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid request body");
                    return;
                }

                Response response = await Recipe.GetRecipe(userId, userData.id);

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
