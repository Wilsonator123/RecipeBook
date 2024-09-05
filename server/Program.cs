using server.Routes;
using server.utils;

var builder = WebApplication.CreateBuilder(args);
var auth = new Auth(builder.Configuration);


// Add configuration to access user-secrets
builder.Configuration.AddUserSecrets<Program>();

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () =>
{
   Auth.GenerateToken("test");
});

app.UseUserRoutes();

app.Run();
