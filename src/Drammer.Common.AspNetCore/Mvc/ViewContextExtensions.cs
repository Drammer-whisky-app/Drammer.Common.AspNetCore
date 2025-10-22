using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drammer.Common.AspNetCore.Mvc;

/// <summary>
/// The view context extensions.
/// </summary>
public static class ViewContextExtensions
{
    /// <summary>
    /// Gets all query parameters from the current request.
    /// </summary>
    /// <param name="viewContext">The view context.</param>
    /// <returns>A dictionary.</returns>
    public static Dictionary<string, string> GetAllQueryParameters(this ViewContext viewContext)
    {
        var query = viewContext.HttpContext.Request.Query;
        return query.ToDictionary(
            x => x.Key,
            x => x.Value.Count > 0 ? x.Value[^1]?.ToString() ?? string.Empty : string.Empty,
            StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Sets or removes a value in a dictionary, returning a new dictionary.
    /// </summary>
    /// <param name="dict">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>A dictionary.</returns>
    public static Dictionary<string, string> WithValue(this Dictionary<string, string> dict, string key, string? value)
    {
        var newDict = new Dictionary<string, string>(dict);
        if (value == null)
        {
            newDict.Remove(key);
        }
        else
        {
            newDict[key] = value;
        }

        return newDict;
    }
}