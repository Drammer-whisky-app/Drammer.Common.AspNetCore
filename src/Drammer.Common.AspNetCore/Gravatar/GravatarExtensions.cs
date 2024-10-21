using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.AspNetCore.Gravatar;

/// <summary>
/// The gravatar extensions.
/// </summary>
public static class GravatarExtensions
{
    /// <summary>
    /// Adds the gravatar service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGravatar(this IServiceCollection services)
    {
        services.AddSingleton<IGravatarService, GravatarService>();
        return services;
    }
}