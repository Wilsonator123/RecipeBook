using server.Routes;
using server.utils;
using server.recipe;

var builder = WebApplication.CreateBuilder(args);
var auth = new Auth(builder.Configuration);

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
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseMiddleware<JwtMiddleware>();

app.MapGet("/health", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.UseUserRoutes();
app.UseRecipeRoutes();

app.Run();
