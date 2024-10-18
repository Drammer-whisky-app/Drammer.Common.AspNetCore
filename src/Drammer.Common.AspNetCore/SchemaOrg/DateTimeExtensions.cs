namespace Drammer.Common.AspNetCore.SchemaOrg;

public static class DateTimeExtensions
{
    /// <summary>
    ///     Returns a HTML5 datetime string.
    /// </summary>
    /// <param name="dateTime">
    ///     The date time.
    /// </param>
    /// <returns>
    ///     The <see cref="string" />.
    /// </returns>
    public static string ToHtml5DateTimeString(this DateTime dateTime)
    {
        return dateTime == default ? string.Empty : dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}