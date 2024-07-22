namespace X.Extensions.Text.Tests;

public class TextHelperTests
{
    [Theory]
    [InlineData("This is a test string.", 10, "...", "This is...")]
    [InlineData("Short", 10, "...", "Short")]
    [InlineData("", 10, "...", "")]
    public void Substring_WithEndPart_ReturnsCorrectSubstring(string input, int length, string endPart, string expected)
    {
        var result = TextHelper.Substring(input, length, endPart);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CleanCharacters_RemovesSystemCharacters()
    {
        var input = "Hello & World!";
        var expected = "hello-world";
        var result = TextHelper.CleanCharacters(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CleanCharacters_HandlesEmptyString()
    {
        var input = "";
        var expected = "";
        var result = TextHelper.CleanCharacters(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Replace_ReplacesCharacters()
    {
        var input = "Hello & World!";
        var forReplace = new[] { "&", "!" };
        var whichReplace = "";
        var expected = "Hello  World";
        var result = TextHelper.Replace(input, forReplace, whichReplace);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("<p>This is a test</p>", "This is a test")]
    [InlineData("<div><p>This is a test</p></div>", "This is a test")]
    [InlineData("<html><body>This is a test</body></html>", "This is a test")]
    public void ToPlainText_ConvertsHtmlToPlainText(string input, string expected)
    {
        var result = TextHelper.ToPlainText(input);
        Assert.Equal(expected, result);
    }
}