using BasicAuth.Authentication;
using BasicAuth.Model;
using BasicAuth.Services.Interfaces;
using BasicAuth.Services.Providers;
using BasicAuth.Storage.Repository.Interfaces;
using BasicAuth.Storage.Repository.Providers;
using Microsoft.AspNetCore.Authentication;

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

    public static IServiceCollection AddApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        return services;
    }
}