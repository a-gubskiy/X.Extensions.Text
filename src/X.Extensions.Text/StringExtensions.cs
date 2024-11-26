using System.Collections.Generic;
using JetBrains.Annotations;

namespace X.Extensions.Text;

/// <summary>
/// Set of extension methods for string
/// </summary>
[PublicAPI]
public static class StringExtensions
{
    /// <summary>
    /// Get a substring from string. The substring starts at a first character position.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Substring(this string text, int length)
    {
        return TextHelper.Substring(text, length);
    }

    /// <summary>
    /// Get a substring from string. The substring starts at a first character position.
    /// If string is longer than length, endPart will be added to the end.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="length"></param>
    /// <param name="endPart"></param>
    /// <returns></returns>
    public static string Substring(this string text, int length, string endPart)
    {
        return TextHelper.Substring(text, length, endPart);
    }

    /// <summary>
    /// Clean system characters from string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string CleanCharacters(this string text)
    {
        return TextHelper.CleanCharacters(text);
    }

    /// <summary>
    /// Replace multiple strings in text with single string
    /// </summary>
    /// <param name="text"></param>
    /// <param name="targets"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static string Replace(this string text, IEnumerable<string> targets, string replacement)
    {
        return TextHelper.Replace(text, targets, replacement);
    }

    /// <summary>
    /// Try to convert HTML to plain text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="saveHtmlLineBreaks">
    /// Save HTML line breaks
    /// </param>
    /// <returns></returns>
    public static string ToPlainText(this string text, bool saveHtmlLineBreaks)
    {
        return TextHelper.ToPlainText(text, saveHtmlLineBreaks);
    }
    
    /// <summary>
    /// Try to convert HTML to plain text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ToPlainText(this string text)
    {
        return TextHelper.ToPlainText(text);
    }

    /// <summary>
    /// Get keywords from text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static string GetKeywords(this string text, int count)
    {
        return TextHelper.GetKeywords(text, count);
    }
    
    /// <summary>
    /// Cut the text to the specified length, preserving the logical block
    /// </summary>
    /// <param name="text"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string CutText(this string text, int maxLength)
    {
        return TextHelper.CutText(text, maxLength);
    }
}


