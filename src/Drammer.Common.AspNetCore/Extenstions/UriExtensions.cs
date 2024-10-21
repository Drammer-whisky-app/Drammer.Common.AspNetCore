﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;

namespace Drammer.Common.AspNetCore.Extenstions;

public static class UriExtensions
{
    /// <summary>
    /// Alters the paging query parameter.
    /// </summary>
    /// <param name="uri">The uri.</param>
    /// <param name="queryStringParameterName">The name of the paging parameter.</param>
    /// <param name="modifier">The modifier, e.g. 1 or -1.</param>
    /// <param name="defaultValue">The default value when the page parameter is not found.</param>
    /// <param name="includeSchemeAndHost">When true the scheme en host are included in the result.</param>
    /// <returns>The new url.</returns>
    public static string AlterPagingQueryParam(
        this Uri uri,
        string queryStringParameterName,
        int modifier,
        int defaultValue,
        bool includeSchemeAndHost = false)
    {
        var baseUri = includeSchemeAndHost
                          ? uri.GetComponents(
                              UriComponents.Scheme | UriComponents.Host | UriComponents.Path,
                              UriFormat.UriEscaped)
                          : uri.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

        var query = QueryHelpers.ParseNullableQuery(uri.Query);
        List<KeyValuePair<string, string>> items;
        if (query != null)
        {
            items = query.SelectMany(
                x => x.Value,
                (col, value) => new KeyValuePair<string, string>(col.Key, value ?? string.Empty)).ToList();

            var pageItem = items
                .SingleOrDefault(x => x.Key.Equals(queryStringParameterName, StringComparison.OrdinalIgnoreCase));
            if (!pageItem.Equals(default(KeyValuePair<string, string>)))
            {
                var pageValue = int.Parse(pageItem.Value);
                items.RemoveAll(x => x.Key.Equals(queryStringParameterName, StringComparison.OrdinalIgnoreCase));
                items.Add(new(queryStringParameterName, (pageValue + modifier).ToString()));
            }
            else
            {
                items.Add(new(queryStringParameterName, defaultValue.ToString()));
            }
        }
        else
        {
            items = new List<KeyValuePair<string, string>>
                        {
                            new(queryStringParameterName, defaultValue.ToString()),
                        };
        }

        var queryBuilder = new QueryBuilder(items);
        return baseUri + queryBuilder.ToQueryString();
    }

    /// <summary>
    /// Replaces the paging query parameter.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="queryStringParameterName"></param>
    /// <param name="value"></param>
    /// <param name="includeSchemeAndHost"></param>
    /// <returns></returns>
    public static string ReplacePagingQueryParam(
        this Uri uri,
        string queryStringParameterName,
        int value,
        bool includeSchemeAndHost = false)
    {
        var baseUri = includeSchemeAndHost
                          ? uri.GetComponents(
                              UriComponents.Scheme | UriComponents.Host | UriComponents.Path,
                              UriFormat.UriEscaped)
                          : uri.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

        var query = QueryHelpers.ParseNullableQuery(uri.Query);
        List<KeyValuePair<string, string>> items;
        if (query != null)
        {
            items = query.SelectMany(
                x => x.Value,
                (col, v) => new KeyValuePair<string, string>(col.Key, v ?? string.Empty)).ToList();

            items.RemoveAll(x => x.Key.Equals(queryStringParameterName, StringComparison.OrdinalIgnoreCase));
            items.Add(new(queryStringParameterName, value.ToString()));
        }
        else
        {
            items = new List<KeyValuePair<string, string>>
                        {
                            new(queryStringParameterName, value.ToString()),
                        };
        }

        var queryBuilder = new QueryBuilder(items);
        return baseUri + queryBuilder.ToQueryString();
    }
}