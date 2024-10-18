using Drammer.Common.AspNetCore.SchemaOrg;

namespace Drammer.Common.AspNetCore.Tests.SchemaOrg;

public sealed class DateTimeExtensionsTests
{
    [Fact]
    public void ToHtml5DateTimeString_WhenDateTimeIsDefault_ReturnsEmptyString()
    {
        // Arrange
        var dateTime = default(DateTime);

        // Act
        var result = dateTime.ToHtml5DateTimeString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToHtml5DateTimeString_WhenDateTimeIsNotDefault_ReturnsFormattedString()
    {
        // Arrange
        var dateTime = new DateTime(2021, 1, 1, 12, 0, 0);

        // Act
        var result = dateTime.ToHtml5DateTimeString();

        // Assert
        Assert.Equal("2021-01-01T12:00:00", result);
    }
}