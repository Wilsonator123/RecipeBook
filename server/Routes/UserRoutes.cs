using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using server.user;
using server.Data;
using server.utils;

namespace server.Routes
{
    public static class UserRoutes
    {
        // Corrected the method name to match the call in Program.cs
        public static IApplicationBuilder UseUserRoutes(this IApplicationBuilder builder)
        {
            return builder.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/user/getUser", async context =>
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var body = await reader.ReadToEndAsync();
                    
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }
                    
                    var userData = JsonSerializer.Deserialize<GetUserRequest>(body);

                    if (userData == null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }

                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(userData);
                    if (!Validator.TryValidateObject(userData, validationContext, validationResults, true))
                    {
                        var errors = validationResults.Select(vr => vr.ErrorMessage).ToArray();
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
                        return;
                    }
                    
                    Response response = await User.GetUser(userData.email, userData.userid);
                    
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
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                });

                endpoints.MapPost("/user/createUser", async context =>
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var body = await reader.ReadToEndAsync();
                    
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }
                    
                    var userData = JsonSerializer.Deserialize<CreateUserRequest>(body);
                    
                    if (userData == null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }
                    
                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(userData);
                    if (!Validator.TryValidateObject(userData, validationContext, validationResults, true))
                    {
                        var errors = validationResults.Select(vr => vr.ErrorMessage).ToArray();
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
                        return;

                    }
                    
                    Response response = await User.CreateUser(userData.email, userData.fname, userData.lname, userData.password);
                    
                    if (response.Status)
                    {
                        string token = Auth.GenerateToken(response.Data.ToString());
                        context = Auth.SetCookie(context, token);
                        context.Response.StatusCode = 201;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                });
                
                endpoints.MapGet("/user/login", async context =>
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var body = await reader.ReadToEndAsync();
                    
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }
                    
                    var userData = JsonSerializer.Deserialize<LoginRequest>(body);

                    if (userData == null)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }

                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(userData);
                    if (!Validator.TryValidateObject(userData, validationContext, validationResults, true))
                    {
                        var errors = validationResults.Select(vr => vr.ErrorMessage).ToArray();
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
                        return;
                    }
                    
                    Response response = await User.Login(userData.email, userData.password);
                    
                    if (response.Status)
                    {
                        string token = Auth.GenerateToken(response.Data.ToString());
                        context = Auth.SetCookie(context, token);
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                });
            });
        }
    }
}
