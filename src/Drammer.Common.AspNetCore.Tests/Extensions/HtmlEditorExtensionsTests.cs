using System.ComponentModel.DataAnnotations;
using Drammer.Common.AspNetCore.Mvc;

namespace Drammer.Common.AspNetCore.Tests.Extensions;

public sealed class HtmlEditorExtensionsTests
{

    [Fact]
    public void CreateOptionList_WithBasicEnum()
    {
        // Act
        var result = HtmlEditorExtensions.CreateOptionList(typeof(BasicEnum));

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Text == nameof(BasicEnum.Value1) && x.IntValue == 0);
        result.Should().Contain(x => x.Text == nameof(BasicEnum.Value2) && x.IntValue == 1);
    }

    [Fact]
    public void CreateOptionList_WithDisplayAnnotatedEnum()
    {
        // Act
        var result = HtmlEditorExtensions.CreateOptionList(typeof(EnumWithDisplay));

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Text == "v1" && x.IntValue == 1);
        result.Should().Contain(x => x.Text == "v2" && x.IntValue == 2);
    }

    [Fact]
    public void CreateOptionList_WithDisplayAndResourceAnnotatedEnum()
    {
        // Act
        var result = HtmlEditorExtensions.CreateOptionList(typeof(EnumWithResource));

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Text == "ResourceValue1" && x.IntValue == 1);
        result.Should().Contain(x => x.Text == "ResourceValue2" && x.IntValue == 2);
    }

    internal enum BasicEnum
    {
        Value1,
        Value2,
    }

    internal enum EnumWithDisplay
    {
        [Display(Name = "v1")]
        Value1 = 1,
        [Display(Name = "v2")]
        Value2 = 2,
    }

    internal enum EnumWithResource
    {
        [Display(Name = "v1", ResourceType = typeof(EnumResource))]
        Value1 = 1,
        [Display(Name = "v2", ResourceType = typeof(EnumResource))]
        Value2 = 2,
    }
}