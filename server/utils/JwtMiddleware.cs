using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using server.utils;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var auth = context.RequestServices.GetRequiredService<Auth>();
        var path = context.Request.Path;
        
        if (path.StartsWithSegments("/user") || path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }
        
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

        context.Items["UserId"] = userId;

        await _next(context);
    }
}