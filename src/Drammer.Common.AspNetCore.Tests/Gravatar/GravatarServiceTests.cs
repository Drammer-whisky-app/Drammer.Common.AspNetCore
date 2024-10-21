using Drammer.Common.AspNetCore.Gravatar;

namespace Drammer.Common.AspNetCore.Tests.Gravatar;

public sealed class GravatarServiceTests
{
    private const string EmailAddress = "noreply@drammer.com";
    private const string EmailHash = "bb413cfd4f91ea54551472f1d0960c82";

    private readonly Fixture _fixture = new();

    [Fact]
    public void Url_ReturnsGravatarUrl()
    {
        // arrange
        var service = new GravatarService();


        // act
        var url = service.Url(EmailAddress);

        // assert
        url.Should().Be($"https://gravatar.com/avatar/{EmailHash}?d=identicon");
    }

    [Fact]
    public void Url_WithSizeRatingAndDefaultImage_ReturnsGravatarUrl()
    {
        // arrange
        var service = new GravatarService();
        var size = _fixture.Create<int>();
        var rating = GravatarRating.Pg;
        var defaultImage = GravatarDefaultImage.Wavatar;

        // act
        var url = service.Url(EmailAddress, size, rating, defaultImage);

        // assert
        url.Should().Be($"https://gravatar.com/avatar/{EmailHash}?s={size}&r=pg&d=wavatar");
    }

    [Fact]
    public void Url_WithCustomUrl_ReturnsGravatarUrl()
    {
        // arrange
        var service = new GravatarService();
        var size = _fixture.Create<int>();
        var rating = GravatarRating.Default;
        var defaultImage = GravatarDefaultImage.Custom;
        var customUrl = _fixture.Create<string>();

        // act
        var url = service.Url(EmailAddress, size, rating, defaultImage, customUrl);

        // assert
        url.Should().Be($"https://gravatar.com/avatar/{EmailHash}?s={size}&d={customUrl}");
    }
}