using Microsoft.AspNetCore.Http;

namespace Drammer.Common.AspNetCore.Extenstions;

public static class RequestExtensions
{
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