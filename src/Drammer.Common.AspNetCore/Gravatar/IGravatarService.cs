using Microsoft.AspNetCore.Html;

namespace Drammer.Common.AspNetCore.Gravatar;

public interface IGravatarService
{
    string Url(
        string email,
        int? size = null,
        GravatarRating rating = GravatarRating.Default,
        GravatarDefaultImage defaultImage = GravatarDefaultImage.Identicon,
        string? customDefaultImageUrl = null);

    HtmlString HtmlSrc(
        string email,
        int? size = null,
        GravatarRating rating = GravatarRating.Default,
        GravatarDefaultImage defaultImage = GravatarDefaultImage.Identicon,
        string? customDefaultImageUrl = null,
        object? htmlAttributes = null);
}