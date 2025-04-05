using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Application.Service.Account;

namespace Blog.Web.Extention;

public static class AuthConfigurations
{
    public static void AddBlogAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                RequireSignedTokens = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
            options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Token validation failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    var accountService = context.HttpContext.RequestServices.GetRequiredService<IAccountService>();
                    var token = new JwtSecurityTokenHandler().ReadJwtToken(context.HttpContext.Request.Headers.Authorization.ToString().Split(' ')[1]);
                    var sid = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

                    if (sid == null || accountService.IsTokenBlackListed(sid.Value))
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.Fail("Token validation failed");
                    }

                    Console.WriteLine("Token validation success");
                    return Task.CompletedTask;
                }
            };
        });
    }

    public static void AddBlogAuthorization()
    {

    }
}
