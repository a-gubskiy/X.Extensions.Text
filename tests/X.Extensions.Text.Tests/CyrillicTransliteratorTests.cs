using X.Extensions.Text.Transliteration;

namespace X.Extensions.Text.Tests;

public class CyrillicTransliteratorTests
{
    private readonly CyrillicTransliterator _transliterator;

    public CyrillicTransliteratorTests()
    {
        _transliterator = new CyrillicTransliterator();
    }

    [Theory]
    [InlineData("привет", "privet")]
    [InlineData("мир", "mir")]
    [InlineData("тест", "test")]
    [InlineData("Щёка", "Shchyoka")]
    [InlineData("Царевна", "Tsarevna")]
    public void ToTransliteration_TransliteratesCyrillicToLatin(string input, string expected)
    {
        var result = _transliterator.ToTransliteration(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("privet", "привет")]
    [InlineData("mir", "мир")]
    [InlineData("test", "тест")]
    [InlineData("Shchyoка", "Щёка")]
    [InlineData("Tsarevna", "Царевна")]
    public void FromTransliteration_TransliteratesLatinToCyrillic(string input, string expected)
    {
        var result = _transliterator.FromTransliteration(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToTransliteration_ReturnsEmptyString_WhenInputIsEmpty()
    {
        var result = _transliterator.ToTransliteration(string.Empty);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FromTransliteration_ReturnsEmptyString_WhenInputIsEmpty()
    {
        var result = _transliterator.FromTransliteration(string.Empty);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToTransliteration_DoesNotAlterText_WhenNoCyrillicCharacters()
    {
        const string input = "hello world";
        var result = _transliterator.ToTransliteration(input);
        Assert.Equal(input, result);
    }

    [Fact]
    public void FromTransliteration_DoesNotAlterText_WhenNoTransliteratedCharacters()
    {
        const string input = "hello world";
        var result = _transliterator.FromTransliteration(input);
        Assert.Equal("хэлло ворлд", result);
    }
}