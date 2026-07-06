using BasicAuth.Authentication;
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
        return services;
    }

    public static IServiceCollection AddBasicAuth(this IServiceCollection services)
    {
        services.AddAuthentication("Basic").AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("Basic", null);
        return services;
    }


}