using Drammer.Common.AspNetCore.Extenstions;

namespace Drammer.Common.AspNetCore.Tests.Extensions;

public sealed class UriExtensionsTests
{
    [Fact]
    public void AlterPagingQueryParam_IncludeHostAlterPage_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint?page=1&pageSize=10");
        var expectedUri = new Uri("http://localhost:5000/api/v1/endpoint?pageSize=10&page=2");

        // Act
        var result = uri.AlterPagingQueryParam("page", 1, 1, true);

        // Assert
        result.Should().Be(expectedUri.ToString());
    }

    [Fact]
    public void AlterPagingQueryParam_IncludeHostUriWithoutPortAlterPage_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost/api/v1/endpoint?page=1&pageSize=10");
        var expectedUri = new Uri("http://localhost/api/v1/endpoint?pageSize=10&page=2");

        // Act
        var result = uri.AlterPagingQueryParam("page", 1, 1, true);

        // Assert
        result.Should().Be(expectedUri.ToString());
    }

    [Fact]
    public void AlterPagingQueryParam_WithoutQuerySTring_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint");
        var expectedUri = new Uri("http://localhost:5000/api/v1/endpoint?page=1");

        // Act
        var result = uri.AlterPagingQueryParam("page", 1, 1, true);

        // Assert
        result.Should().Be(expectedUri.ToString());
    }

    [Fact]
    public void AlterPagingQueryParam_ExcludeHostAlterPage_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint?page=1&pageSize=10");
        var expectedUrl = "api/v1/endpoint?pageSize=10&page=2";

        // Act
        var result = uri.AlterPagingQueryParam("page", 1, 1);

        // Assert
        result.Should().Be(expectedUrl);
    }

    [Fact]
    public void AlterPagingQueryParam_ExcludeHostWithoutPage_ShouldReturnNewUriWithDefaultValue()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint?pageSize=10");
        var expectedUrl = "api/v1/endpoint?pageSize=10&page=10";

        // Act
        var result = uri.AlterPagingQueryParam("page", 1, 10);

        // Assert
        result.Should().Be(expectedUrl);
    }

    [Fact]
    public void ReplacePagingQueryParam_IncludeHostReplacePage_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint?page=1&pageSize=10");
        var expectedUri = new Uri("http://localhost:5000/api/v1/endpoint?pageSize=10&page=20");

        // Act
        var result = uri.ReplacePagingQueryParam("page", 20, true);

        // Assert
        result.Should().Be(expectedUri.ToString());
    }

    [Fact]
    public void ReplacePagingQueryParam_IncludeHostUriWithoutPortReplacePage_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost/api/v1/endpoint?page=1&pageSize=10");
        var expectedUri = new Uri("http://localhost/api/v1/endpoint?pageSize=10&page=20");

        // Act
        var result = uri.ReplacePagingQueryParam("page", 20, true);

        // Assert
        result.Should().Be(expectedUri.ToString());
    }

    [Fact]
    public void ReplacePagingQueryParam_WithoutQueryString_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint");
        var expectedUri = new Uri("http://localhost:5000/api/v1/endpoint?page=20");

        // Act
        var result = uri.ReplacePagingQueryParam("page", 20, true);

        // Assert
        result.Should().Be(expectedUri.ToString());
    }

    [Fact]
    public void ReplacePagingQueryParam_ExcludeHostReplacePage_ShouldReturnNewUri()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint?page=1&pageSize=10");
        var expectedUrl = "api/v1/endpoint?pageSize=10&page=20";

        // Act
        var result = uri.ReplacePagingQueryParam("page", 20);

        // Assert
        result.Should().Be(expectedUrl);
    }

    [Fact]
    public void ReplacePagingQueryParam_ExcludeHostWithoutPage_ShouldReturnNewUriWithDefaultValue()
    {
        // Arrange
        var uri = new Uri("http://localhost:5000/api/v1/endpoint?pageSize=10");
        var expectedUrl = "api/v1/endpoint?pageSize=10&page=20";

        // Act
        var result = uri.ReplacePagingQueryParam("page", 20);

        // Assert
        result.Should().Be(expectedUrl);
    }
}