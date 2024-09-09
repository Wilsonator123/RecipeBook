using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace server.utils;

class Auth
{
    private static IConfiguration _configuration;

    public Auth(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GenerateToken(string username)
    {
        var secret = _configuration["Jwt:Secret"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, username)
                
            }),
            Expires = DateTime.UtcNow.AddDays(14),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public static string ValidateToken(string token)
    {
        var secret = _configuration["Jwt:Secret"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var usernameClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) 
                                ?? jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid");

            if (usernameClaim == null)
            {
                return null;
            }

            return usernameClaim.Value;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}