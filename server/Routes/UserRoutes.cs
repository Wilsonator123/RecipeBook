using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using server.user;
using server.Data;
using server.utils;

namespace server.Routes
{
    public static class UserRoutes
    {
        public static IApplicationBuilder UseUserRoutes(this IApplicationBuilder builder)
        {
            return builder.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/user/getUser", async context =>
                {
                    var auth = context.RequestServices.GetRequiredService<Auth>();
                    var cookie = CookieHelper.GetUserIdFromCookies(context);

                    if (string.IsNullOrWhiteSpace(cookie))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("User ID cookie not found");
                        return;
                    }

                    string userId = auth.ValidateToken(cookie);

                    if (string.IsNullOrWhiteSpace(userId))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid token");
                        return;
                    }

                    Response response = await User.GetUser(userId);

                    

                    if (response.Status)
                    {
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        
                        if (response.Data is object[] data && data.Length >= 4)
                        {
                            response.Data = new UserResponse
                            {
                                id = data[0]?.ToString(),
                                email = data[1]?.ToString(),
                                fname = data[2]?.ToString(),
                                lname = data[3]?.ToString()
                            };
                        }
                        
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
                    var auth = context.RequestServices.GetRequiredService<Auth>();
                    var (isValid, userData) = await ValidateBody.Validate<CreateUserRequest>(context);
                    if (!isValid)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }
                    
                    
                    Response response = await User.CreateUser(userData.email, userData.fname, userData.lname, userData.password);
                    
                    if (response.Status)
                    {
                        Console.WriteLine(response);
                        string token = auth.GenerateToken(response.Data.ToString());
                        context = CookieHelper.SetCookie(context, token);
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
                
                endpoints.MapPost("/user/login", async context =>
                {   
                    var auth = context.RequestServices.GetRequiredService<Auth>();
                    var (isValid, userData) = await ValidateBody.Validate<LoginRequest>(context);
                    
                    if (!isValid)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid request body");
                        return;
                    }
                    
                    Response response = await User.Login(userData.email, userData.password);
                    
                    if (response.Status)
                    {
                        Console.WriteLine(response.Data);
                        string token = auth.GenerateToken(response.Data.ToString());
                        context = CookieHelper.SetCookie(context, token);
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
