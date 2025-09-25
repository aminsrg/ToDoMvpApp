using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ToDoMvpApp.Api.Middleware;

public class JwtMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            try
            {
                var key = context.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Key"];
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = context.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Issuer"] ?? "MyApp",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                context.User = principal;

                // Store UserId in HttpContext.Items for handlers
                var userId = principal.FindFirst("UserId")?.Value;
                if (!string.IsNullOrEmpty(userId))
                    context.Items["UserId"] = userId;
            }
            catch
            {
                // log here!
            }
        }

        await _next(context);
    }
}
