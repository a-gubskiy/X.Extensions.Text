using System.Collections.Generic;
using JetBrains.Annotations;

namespace X.Extensions.Text;

/// <summary>
/// A set of extension methods for <see cref="string"/> that delegate to <c>TextHelper</c>.
/// These methods provide common text operations as instance-style extensions.
/// </summary>
[PublicAPI]
public static class StringExtensions
{
    /// <summary>
    /// Returns a substring of <paramref name="text"/> starting at the first character up to
    /// the specified <paramref name="length"/>. This method delegates to <c>TextHelper.Substring</c>.
    /// </summary>
    /// <param name="text">The source string. May be <c>null</c> or empty.</param>
    /// <param name="length">The maximum length of the substring to return.</param>
    /// <returns>
    /// A substring of <paramref name="text"/>. Behaviour when <paramref name="text"/> is shorter than
    /// <paramref name="length"/> is delegated to <c>TextHelper.Substring</c>.
    /// </returns>
    public static string Substring(this string text, int length)
    {
        return TextHelper.Substring(text, length);
    }

    /// <summary>
    /// Returns a substring of <paramref name="text"/> starting at the first character up to
    /// the specified <paramref name="length"/>. If the original text is longer than <paramref name="length"/>,
    /// the <paramref name="endPart"/> string is appended. Delegates to <c>TextHelper.Substring</c>.
    /// </summary>
    /// <param name="text">The source string. May be <c>null</c> or empty.</param>
    /// <param name="length">The maximum length of the substring before appending <paramref name="endPart"/>.</param>
    /// <param name="endPart">The string to append when the original text is longer than <paramref name="length"/>.</param>
    /// <returns>
    /// A possibly truncated version of <paramref name="text"/> with <paramref name="endPart"/> appended when appropriate.
    /// </returns>
    public static string Substring(this string text, int length, string endPart)
    {
        return TextHelper.Substring(text, length, endPart);
    }

    /// <summary>
    /// Removes or normalizes system/control characters from <paramref name="text"/>.
    /// Delegates to <c>TextHelper.CleanCharacters</c>.
    /// </summary>
    /// <param name="text">The source string to clean. May be <c>null</c>.</param>
    /// <returns>The cleaned string. If <paramref name="text"/> is <c>null</c>, behaviour is delegated to <c>TextHelper</c>.</returns>
    public static string CleanCharacters(this string text)
    {
        return TextHelper.CleanCharacters(text);
    }

    /// <summary>
    /// Replaces multiple target substrings in <paramref name="text"/> with a single replacement string.
    /// Delegates to <c>TextHelper.Replace</c>.
    /// </summary>
    /// <param name="text">The source string in which replacements are performed.</param>
    /// <param name="targets">A collection of target substrings to replace. Implementations should handle <c>null</c> or empty collections appropriately.</param>
    /// <param name="replacement">The string to use as replacement for each target.</param>
    /// <returns>A new string with all occurrences of the specified targets replaced by <paramref name="replacement"/>.</returns>
    public static string Replace(this string text, IEnumerable<string> targets, string replacement)
    {
        return TextHelper.Replace(text, targets, replacement);
    }

    /// <summary>
    /// Converts HTML content in <paramref name="text"/> to plain text. Optionally preserves HTML line breaks.
    /// Delegates to <c>TextHelper.ToPlainText</c>.
    /// </summary>
    /// <param name="text">The HTML string to convert.</param>
    /// <param name="saveHtmlLineBreaks">If <c>true</c>, HTML line breaks are preserved in the output where possible.</param>
    /// <returns>A plain-text representation of the input HTML.</returns>
    public static string ToPlainText(this string text, bool saveHtmlLineBreaks)
    {
        return TextHelper.ToPlainText(text, saveHtmlLineBreaks);
    }

    /// <summary>
    /// Converts HTML content in <paramref name="text"/> to plain text using default options.
    /// Delegates to <c>TextHelper.ToPlainText</c>.
    /// </summary>
    /// <param name="text">The HTML string to convert.</param>
    /// <returns>A plain-text representation of the input HTML.</returns>
    public static string ToPlainText(this string text)
    {
        return TextHelper.ToPlainText(text);
    }

    /// <summary>
    /// Extracts keywords from <paramref name="text"/>. Delegates to <c>TextHelper.GetKeywords</c>.
    /// </summary>
    /// <param name="text">The source text from which to extract keywords.</param>
    /// <param name="count">The maximum number of keywords to return.</param>
    /// <returns>
    /// A string representation of the extracted keywords. The exact format and behavior (e.g. delimiter)
    /// are defined by <c>TextHelper.GetKeywords</c>.
    /// </returns>
    public static string GetKeywords(this string text, int count)
    {
        return TextHelper.GetKeywords(text, count);
    }

    /// <summary>
    /// Cuts <paramref name="text"/> to the specified <paramref name="maxLength"/>, attempting to preserve logical blocks
    /// (for example, not cutting in the middle of a word or sentence) where supported by the helper implementation.
    /// Delegates to <c>TextHelper.CutText</c>.
    /// </summary>
    /// <param name="text">The source text to cut.</param>
    /// <param name="maxLength">The maximum allowed length of the returned text.</param>
    /// <returns>
    /// A possibly shortened version of <paramref name="text"/> that does not exceed <paramref name="maxLength"/>.
    /// Exact trimming logic is implemented by <c>TextHelper.CutText</c>.
    /// </returns>
    public static string CutText(this string text, int maxLength)
    {
        return TextHelper.CutText(text, maxLength);
    }
}