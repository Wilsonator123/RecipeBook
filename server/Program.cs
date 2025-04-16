using server.Routes;
using server.utils;
using server.recipe;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddSingleton<Auth>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Adjust the origin to match your frontend's URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
// Add configuration to access user-secrets


builder.Services.AddAuthentication();

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseMiddleware<JwtMiddleware>();

app.MapGet("/health", async context =>
{
    WebscrapeResponse response = await WebScrape.GetRecipe("https://www.recipetineats.com/vietnamese-pork-noodle-bowls");
    await context.Response.WriteAsJsonAsync(response);
});

app.UseUserRoutes();
app.UseRecipeRoutes();

app.Run();
