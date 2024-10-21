namespace Drammer.Common.AspNetCore.Gravatar;

/// <summary>
/// The rating of the Gravatar image to display.
/// </summary>
public enum GravatarRating
{
    /// <summary>
    /// The default value as specified by the Gravatar service.  That is, no rating value is specified
    /// with the request.  At the time of authoring, the default level was <see cref="G"/>.
    /// </summary>
    Default,

    /// <summary>
    /// Suitable for display on all websites with any audience type.  This is the default.
    /// </summary>
    G,

    /// <summary>
    /// May contain rude gestures, provocatively dressed individuals, the lesser swear words, or mild violence.
    /// </summary>
    Pg,

    /// <summary>
    /// May contain such things as harsh profanity, intense violence, nudity, or hard drug use.
    /// </summary>
    R,

    /// <summary>
    /// May contain hardcore sexual imagery or extremely disturbing violence.
    /// </summary>
    X
}