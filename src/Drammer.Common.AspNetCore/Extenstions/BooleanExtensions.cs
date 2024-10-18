using Microsoft.AspNetCore.Html;

namespace Drammer.Common.AspNetCore.Extenstions;

public static class BooleanExtensions
{
    public static IHtmlContent ToHtml(this bool value, bool showFalse = true)
    {
        if (value)
        {
            return new HtmlString("✅");
        }

        return showFalse ? new HtmlString("⛔") : new HtmlString(string.Empty);
    }
}