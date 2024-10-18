using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.AspNetCore.Mvc;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProxyUrlHelper(this IServiceCollection services)
    {
        services.AddScoped<IUrlHelper, ProxyUrlHelper>();
        return services;
    }
}