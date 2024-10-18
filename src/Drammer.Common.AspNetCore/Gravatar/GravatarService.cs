using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Drammer.Common.AspNetCore.Gravatar;

/// <summary>
/// HtmlHelpers for Gravatar, see http://stackoverflow.com/questions/3561477/asp-net-mvc-helper-for-accessing-gravatar-images
/// </summary>
public sealed class GravatarService : IGravatarService
{
    private const string BaseUrl = "https://secure.gravatar.com/avatar/";

    /// <summary>
    /// Creates the Gravatar url
    /// </summary>
    /// <param name="email"></param>
    /// <param name="size"></param>
    /// <param name="rating"></param>
    /// <param name="defaultImage"></param>
    /// <param name="customDefaultImageUrl"></param>
    /// <returns></returns>
    public string Url(
        string email,
        int? size = null,
        GravatarRating rating = GravatarRating.Default,
        GravatarDefaultImage defaultImage = GravatarDefaultImage.Identicon,
        string? customDefaultImageUrl = null)
    {
        var url = new StringBuilder(BaseUrl);
        url.Append(GetEmailHash(email));

        var isFirst = true;

        void AddParam(string p, string v)
        {
            url.Append(isFirst ? '?' : '&');
            isFirst = false;
            url.Append(p);
            url.Append('=');
            url.Append(v);
        }

        if (size != null)
        {
            if (size is < 1 or > 512)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(size),
                    size,
                    "Must be null or between 1 and 512, inclusive.");
            }

            AddParam("s", size.Value.ToString());
        }

        if (rating != GravatarRating.Default)
        {
            AddParam("r", rating.ToString().ToLower());
        }

        if (defaultImage != GravatarDefaultImage.Default)
        {
            if (defaultImage == GravatarDefaultImage.Custom)
            {
                if (!string.IsNullOrEmpty(customDefaultImageUrl))
                {
                    AddParam("d", System.Web.HttpUtility.UrlEncode(customDefaultImageUrl));
                }
                else
                {
                    defaultImage = GravatarDefaultImage.Default;
                }
            }
            else if (defaultImage == GravatarDefaultImage.Http404)
            {
                AddParam("d", "404");
            }
            else if (defaultImage == GravatarDefaultImage.Identicon)
            {
                AddParam("d", "identicon");
            }
            else if (defaultImage == GravatarDefaultImage.MonsterId)
            {
                AddParam("d", "monsterid");
            }
            else if (defaultImage == GravatarDefaultImage.MysteryMan)
            {
                AddParam("d", "mm");
            }
            else if (defaultImage == GravatarDefaultImage.Wavatar)
            {
                AddParam("d", "wavatar");
            }
        }

        return url.ToString();
    }

    /// <summary>
    /// Creates HTML for an <c>img</c> element that presents a Gravatar icon.
    /// </summary>
    /// <param name="html">The <see cref="HtmlHelper"/> upon which this extension method is provided.</param>
    /// <param name="email">The email address used to identify the icon.</param>
    /// <param name="size">An optional parameter that specifies the size of the square image in pixels.</param>
    /// <param name="rating">An optional parameter that specifies the safety level of allowed images.</param>
    /// <param name="defaultImage">An optional parameter that controls what image is displayed for email addresses that don't have associated Gravatar icons.</param>
    /// <param name="htmlAttributes">An optional parameter holding additional attributes to be included on the <c>img</c> element.</param>
    /// <returns>An HTML string of the <c>img</c> element that presents a Gravatar icon.</returns>
    public HtmlString HtmlSrc(
        string email,
        int? size = null,
        GravatarRating rating = GravatarRating.Default,
        GravatarDefaultImage defaultImage = GravatarDefaultImage.Identicon,
        string? customDefaultImageUrl = null,
        object? htmlAttributes = null)
    {
        var tag = new TagBuilder("img");
        tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
        tag.Attributes.Add("src", Url(email, size, rating, defaultImage, customDefaultImageUrl));

        if (size != null)
        {
            tag.Attributes.Add("width", size.ToString());
            tag.Attributes.Add("height", size.ToString());
        }

        return new HtmlString(tag.ToString());
    }

    private static string GetEmailHash(string? email)
    {
        if (email == null)
        {
            return new string('0', 32);
        }

        email = email.Trim().ToLower();

        var emailBytes = Encoding.ASCII.GetBytes(email);
        var hashBytes = MD5.Create().ComputeHash(emailBytes);

        Debug.Assert(hashBytes.Length == 16, "hashBytes.Length == 16");

        var hash = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hash.Append(b.ToString("x2"));
        }

        return hash.ToString();
    }
}