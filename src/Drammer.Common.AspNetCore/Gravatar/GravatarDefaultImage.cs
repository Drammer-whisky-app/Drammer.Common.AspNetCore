namespace Drammer.Common.AspNetCore.Gravatar;

public enum GravatarDefaultImage
{
    /// <summary>
    /// The default value image.  That is, the image returned when no specific default value is included
    /// with the request.  At the time of authoring, this image is the Gravatar icon.
    /// </summary>
    Default,

    /// <summary>
    /// Do not load any image if none is associated with the email hash, instead return an HTTP 404 (File Not Found) response.
    /// </summary>
    Http404,

    /// <summary>
    /// A simple, cartoon-style silhouetted outline of a person (does not vary by email hash).
    /// </summary>
    MysteryMan,

    /// <summary>
    /// A geometric pattern based on an email hash.
    /// </summary>
    Identicon,

    /// <summary>
    /// A generated 'monster' with different colors, faces, etc.
    /// </summary>
    MonsterId,

    /// <summary>
    /// Generated faces with differing features and backgrounds.
    /// </summary>
    Wavatar,

    /// <summary>
    /// Custom avatar URL.
    /// </summary>
    Custom
}