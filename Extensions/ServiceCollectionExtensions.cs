using System.Text;
using BasicAuth.Authentication;
using BasicAuth.Model;
using BasicAuth.Services.Interfaces;
using BasicAuth.Services.Providers;
using BasicAuth.Storage.Repository.Interfaces;
using BasicAuth.Storage.Repository.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BasicAuth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserEntityRepository, UserEntityRepository>();
        services.AddScoped<IProductEntityRepository, ProductEntityRepository>();
        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }

    public static IServiceCollection AddBasicAuth(this IServiceCollection services)
    {
        services.AddAuthentication("Basic").AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("Basic", null);
        return services;
    }

    public static IServiceCollection AddBearerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration.GetValue<string>("JwtSettings:Key");
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("No secret key provided");

        var encodedKey = Encoding.UTF8.GetBytes(key);

        var audience = configuration.GetValue<string>("JwtSettings:Audience");
        if (string.IsNullOrWhiteSpace(audience)) throw new ArgumentException("No audience Provided");

        var issuer = configuration.GetValue<string>("JwtSettings:Issuer");
        if (string.IsNullOrWhiteSpace(issuer)) throw new ArgumentException("No issuer provided");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAlgorithms = new string[] { SecurityAlgorithms.HmacSha256 },
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(encodedKey)
            };
        });
        return services;

    }
    public static IServiceCollection AddApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        return services;
    }
}