using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.AspNetCore.Gravatar;

public static class GravatarExtensions
{
    public static IServiceCollection AddGravatar(this IServiceCollection services)
    {
        services.AddSingleton<IGravatarService, GravatarService>();
        return services;
    }
}