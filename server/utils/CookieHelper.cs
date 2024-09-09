// utils/CookieHelper.cs
using Microsoft.AspNetCore.Http;

namespace server.utils;

public static class CookieHelper
{
    public static string GetUserIdFromCookies(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("userid", out var userId))
        {
            return userId;
        }

        return null;
    }

    public static HttpContext SetCookie(HttpContext context, string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(14)
        };
        context.Response.Cookies.Append("userid", token, cookieOptions);

        return context;
    }
}
