using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.AspNetCore.Mvc;

/// <summary>
/// The service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the proxy URL helper.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddProxyUrlHelper(this IServiceCollection services)
    {
        services.AddScoped<IUrlHelper, ProxyUrlHelper>();
        return services;
    }
}