using Xunit;

namespace X.Extensions.Text.Tests;


public class HtmlConverterTests
{
    [Theory]
    [InlineData("<br>This is a test", "This is a test")]
    [InlineData("<p>This is a test</p>", "This is a test<br />")]
    [InlineData("<P>This is a test</P>", "This is a test<br />")]
    [InlineData("Hello<br>World", "Hello<br />World")]
    [InlineData("Hello<br/><br/>World", "Hello<br />World")]
    [InlineData("<p>Hello<br/><br/>World", "Hello<br />World")]
    public void ToPlainText_WithPreserveLineBreaks_ShouldConvertHtmlToPlainText_WithLineBreaks(string input, string expected)
    {
        // Act
        var result = TextHelper.ToPlainText(input, preserveLineBreaks: true);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("<br>This is a test", "This is a test")]
    [InlineData("<p>This is a test</p>", "This is a test")]
    [InlineData("Hello<br>World", "HelloWorld")]
    public void ToPlainText_WithoutPreserveLineBreaks_ShouldConvertHtmlToPlainText_WithoutLineBreaks(string input, string expected)
    {
        // Act
        var result = TextHelper.ToPlainText(input, preserveLineBreaks: false);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToPlainText_WithMultipleConsecutiveLineBreaks_ShouldCollapseIntoSingleBreak()
    {
        // Arrange
        var input = "<br><br><br>This is a test<p></p>";

        // Act
        var result = TextHelper.ToPlainText(input, preserveLineBreaks: true);

        // Assert
        var expected = "This is a test<br />";
        Assert.Equal(expected, result);
    }
}
