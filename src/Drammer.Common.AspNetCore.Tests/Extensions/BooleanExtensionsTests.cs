using Drammer.Common.AspNetCore.Extenstions;

namespace Drammer.Common.AspNetCore.Tests.Extensions;

public sealed class BooleanExtensionsTests
{
    [Theory]
    [InlineData(true, "✅")]
    [InlineData(false, "⛔")]
    public void ToHtml_ReturnsHtmlString(bool value, string expected)
    {
        // act
        var result = value.ToHtml();

        // assert
        result.Should().NotBeNull();
        result.ToString().Should().Contain(expected);
    }
}