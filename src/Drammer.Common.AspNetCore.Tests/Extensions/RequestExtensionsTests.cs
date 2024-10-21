using Drammer.Common.AspNetCore.Extenstions;
using Microsoft.AspNetCore.Http;

namespace Drammer.Common.AspNetCore.Tests.Extensions;

public sealed class RequestExtensionsTests
{
    [Fact]
    public void GetReferer_WhenRequestIsNull_ReturnsNull()
    {
        // arrange
        HttpRequest? request = null;

        // act
        var result = request.GetReferer();

        // assert
        Assert.Null(result);
    }

    [Fact]
    public void GetReferer_WhenRequestHeadersDoesNotContainReferer_ReturnsNull()
    {
        // arrange
        var request = new DefaultHttpContext().Request;

        // act
        var result = request.GetReferer();

        // assert
        Assert.Null(result);
    }

    [Fact]
    public void GetReferer_WhenRequestHeadersContainsReferer_ReturnsRefererPathAndQuery()
    {
        // arrange
        var request = new DefaultHttpContext().Request;
        request.Headers.Append("Referer", "https://example.com/path?query");

        // act
        var result = request.GetReferer();

        // assert
        Assert.Equal("/path?query", result);
    }
}