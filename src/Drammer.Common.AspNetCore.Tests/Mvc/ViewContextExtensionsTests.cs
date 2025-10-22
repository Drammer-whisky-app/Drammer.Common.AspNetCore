using Drammer.Common.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drammer.Common.AspNetCore.Tests.Mvc;

public sealed class ViewContextExtensionsTests
{
    [Fact]
    public void GetAllQueryParameters_ReturnsDictionary()
    {
        // Arrange
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                QueryString = new QueryString("?key1=value1&key2=value2")
            }
        };

        var viewContext = new ViewContext
        {
            HttpContext = httpContext,
        };

        // Act
        var result = viewContext.GetAllQueryParameters();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
        });
    }

    [Fact]
    public void GetAllQueryParameters_WithDuplicateKey_ReturnsDictionary()
    {
        // Arrange
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                QueryString = new QueryString("?key1=value1&key1=value2")
            }
        };

        var viewContext = new ViewContext
        {
            HttpContext = httpContext,
        };

        // Act
        var result = viewContext.GetAllQueryParameters();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            { "key1", "value1" },
        });
    }

    [Fact]
    public void WithValue_AddsValue()
    {
        // Arrange
        var dict = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
        };

        // Act
        var result = dict.WithValue("key3", "value3");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
            { "key3", "value3" },
        });
    }

    [Fact]
    public void WithValue_UpdatesValue()
    {
        // Arrange
        var dict = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
        };

        // Act
        var result = dict.WithValue("key2", "value3");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value3" },
        });
    }

    [Fact]
    public void WithValue_RemovesValue()
    {
        // Arrange
        var dict = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" },
        };

        // Act
        var result = dict.WithValue("key2", null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            { "key1", "value1" },
        });
    }
}