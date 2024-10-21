using Microsoft.AspNetCore.Http;

namespace Drammer.Common.AspNetCore.Extenstions;

/// <summary>
/// The request extensions.
/// </summary>
public static class RequestExtensions
{
    /// <summary>
    /// Gets the referer.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <returns>A <see cref="string"/>.</returns>
    public static string? GetReferer(this HttpRequest? request)
    {
        if (request?.Headers != null && request.Headers.TryGetValue("Referer", out var refererUrl))
        {
            var refererUrlString = refererUrl.ToString();
            if (!string.IsNullOrWhiteSpace(refererUrlString))
            {
                var baseUri = new Uri(refererUrlString);
                return baseUri.PathAndQuery;
            }
        }

        return null;
    }
}