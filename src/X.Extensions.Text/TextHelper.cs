using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace X.Extensions.Text;

/// <summary>
/// Class contains methods to simplify common tasks working with text.
/// Provides utility functions for substring extraction, cleaning characters,
/// HTML-to-plain-text conversion, keyword extraction and truncation logic.
/// </summary>
[PublicAPI]
public static class TextHelper
{
    /// <summary>
    /// Collection of characters and small tokens treated as "system" or separators
    /// when cleaning or tokenizing text. Used by methods such as <see cref="CleanCharacters"/>
    /// and <see cref="Replace(string, IEnumerable{string}, string)"/>.
    /// </summary>
    public static readonly IReadOnlyCollection<string> SystemCharacters = new[]
    {
        "&", "?", "^", ":", "/", "\\", "@", "$", "(", ")", "+", "[",
        "]", "{", "}", "%", "~", ">", "<", "=", "*", "“", "\"", "!",
        "”", "«", "»", ".", ",", "#", "§", "quot;", "--", ";", "\r", "\n", "\t",
        "..."
    };

    /// <summary>
    /// Retrieves a substring from the start of the provided text.
    /// </summary>
    /// <param name="text">The source text. If null or empty, an empty string is returned.</param>
    /// <param name="length">The maximum length of the returned substring including any <paramref name="endPart"/>.</param>
    /// <returns>
    /// A substring starting at index 0. If <paramref name="text"/> is shorter than or equal to <paramref name="length"/>,
    /// the original <paramref name="text"/> is returned. Otherwise a shortened string is returned with no trailing marker.
    /// </returns>
    public static string Substring(string text, int length)
    {
        return Substring(text, length, string.Empty);
    }

    /// <summary>
    /// Retrieves a substring from the start of the provided text and appends an end marker if the text was truncated.
    /// </summary>
    /// <param name="text">The source text. If null or empty, an empty string is returned.</param>
    /// <param name="length">
    /// The maximum length of the returned string including the <paramref name="endPart"/>. If <paramref name="length"/> is less than
    /// <paramref name="endPart"/>.Length this method will attempt to call <see cref="string.Substring(int, int)"/> with a negative length
    /// and therefore may throw <see cref="ArgumentOutOfRangeException"/>. Callers should ensure <paramref name="length"/> is large enough.
    /// </param>
    /// <param name="endPart">Text appended to indicate truncation (for example "..." ). This is included within the requested <paramref name="length"/>.</param>
    /// <returns>
    /// The resulting substring. If truncation is performed the returned value will be the first (length - endPart.Length) characters followed by <paramref name="endPart"/>.
    /// If <paramref name="text"/> fits into <paramref name="length"/>, <paramref name="text"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown by <see cref="string.Substring(int, int)"/> if computed length is negative.</exception>
    public static string Substring(string text, int length, string endPart)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        if (text.Length > length)
        {
            return text.Substring(0, length - endPart.Length) + endPart;
        }

        return text;
    }
        
    /// <summary>
    /// Removes or replaces common system characters and produces a URL-friendly slug.
    /// </summary>
    /// <param name="text">Input text to be cleaned. If null, behavior depends on underlying methods (may throw).</param>
    /// <returns>
    /// A cleaned, lower-cased, trimmed string where system characters have been replaced with hyphens and
    /// multiple consecutive spaces have been collapsed. The final result uses hyphens ('-') as separators.
    /// </returns>
    /// <remarks>
    /// This method uses <see cref="SystemCharacters"/> to determine which tokens to replace with a space,
    /// collapses doubled spaces and replaces remaining spaces with hyphens. It also lowercases the result.
    /// </remarks>
    public static string CleanCharacters(string text)
    {
        const string space = " ";

        var result = Replace(text, SystemCharacters, space);

        var doubleSpace = string.Format("{0}{0}", space);

        while (result.Contains(doubleSpace))
        {
            result = result.Replace(doubleSpace, space).Trim();
        }

        result = result.Trim().ToLower().Replace(space, "-");

        return result;
    }

    /// <summary>
    /// Replaces all occurrences of the specified target strings in the input text with the provided replacement string.
    /// </summary>
    /// <param name="text">The input string in which replacements will be made. If null, <paramref name="targets"/>.Aggregate will throw.</param>
    /// <param name="targets">An enumerable collection of strings to be replaced. Each target is replaced using <see cref="string.Replace(string, string)"/>.</param>
    /// <param name="replacement">The string that will replace each target string.</param>
    /// <returns>The modified string with all specified targets replaced by the replacement string.</returns>
    /// <remarks>
    /// Replacements are applied sequentially in the order provided by <paramref name="targets"/>.
    /// </remarks>
    public static string Replace(string text, IEnumerable<string> targets, string replacement)
    {
        return targets.Aggregate(text, (current, x) => current.Replace(x, replacement));
    }

    /// <summary>
    /// Convert HTML to plain text while preserving HTML line break tags as &lt;br /&gt; elements.
    /// </summary>
    /// <param name="text">The HTML content to be converted.</param>
    /// <param name="preserveLineBreaks">If true, HTML line break tags (&lt;br&gt;, &lt;p&gt;, etc.) are preserved and returned as &lt;br /&gt; in the result; otherwise line breaks are removed.</param>
    /// <returns>The plain text version of the input HTML. When <paramref name="preserveLineBreaks"/> is true, returned string will include literal &lt;br /&gt; tags where line breaks were preserved.</returns>
    /// <remarks>
    /// This overload temporarily replaces known line-break tags with a placeholder to prevent them from being stripped
    /// by the general HTML-to-text conversion, then restores them as &lt;br /&gt;.
    /// </remarks>
    public static string ToPlainText(string text, bool preserveLineBreaks)
    {
        if (!preserveLineBreaks)
        {
            return ToPlainText(text);
        }

        const string lineBreakPlaceholder = "[[LINE_BREAK]]";

        var lineBreakTags = new[]
        {
            "<br>", "<br/>", "<br />", "<BR>", "<BR/>", "<BR />",
            "<P>", "<P/>", "<P />", "</P>", "<p>", "<p/>", "<p />"
        };

        // Replace all line break tags with the placeholder
        foreach (var tag in lineBreakTags)
        {
            text = Regex.Replace(text, tag, lineBreakPlaceholder, RegexOptions.IgnoreCase);
        }

        // Collapse consecutive placeholders to a single one
        while (text.Contains($"{lineBreakPlaceholder}{lineBreakPlaceholder}"))
        {
            text = text.Replace($"{lineBreakPlaceholder}{lineBreakPlaceholder}", $"{lineBreakPlaceholder}");
        }

        text = TrimLineBreaksFromStart(text, lineBreakPlaceholder);
        
        // Convert the HTML to plain text
        text = ToPlainText(text);

        // Replace placeholders back with <br /> tags
        return text.Replace(lineBreakPlaceholder, "<br />");
    }
    
    /// <summary>
    /// Removes leading occurrences of a line break placeholder from the start of the text.
    /// </summary>
    /// <param name="text">Input string from which leading placeholders should be removed.</param>
    /// <param name="lineBreakPlaceholder">Placeholder string that represents a line break. Defaults to <c>"[[LINE_BREAK]]"</c>.</param>
    /// <returns>The input string with leading occurrences of <paramref name="lineBreakPlaceholder"/> removed. If <paramref name="text"/> is null or empty, it is returned unchanged.</returns>
    public static string TrimLineBreaksFromStart(string text, string lineBreakPlaceholder = "[[LINE_BREAK]]")
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        while (text.StartsWith(lineBreakPlaceholder))
        {
            text = text.Substring(lineBreakPlaceholder.Length);
        }

        return text;
    }

    /// <summary>
    /// Convert HTML content to a plain text representation by removing tags and common markup fragments.
    /// </summary>
    /// <param name="text">The HTML or markup input. If null, methods operating on it may throw.</param>
    /// <returns>
    /// A plain-text string with many HTML tags, comments and common entity tokens removed or replaced with spaces.
    /// The returned string is trimmed of leading and trailing whitespace.
    /// </returns>
    /// <remarks>
    /// The implementation uses a set of regular expressions to remove comments, titles, meta/link/style blocks,
    /// inline styles and various markup fragments, then replaces remaining special tokens with spaces.
    /// This method is heuristic and not a full HTML parser; it is intended for simple conversions where full fidelity is not required.
    /// </remarks>
    public static string ToPlainText(string text)
    {
        var patternCollection = new[]
        {
            @"<!--(\w|\W)+?-->",
            @"<title>(\w|\W)+?</title>",
            @"\s?class=\w+",
            @"\s+style='[^']+'",
            @"<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|body|/?body|/?span|!\[)[^>]*?>",
            @"(<[^>]+>)+&nbsp;(</\w+>)+",
            @"\s+v:\w+=""[^""]+""",
            @"(\n\r){2,}"
        };

        var sb = new StringBuilder(text);

        Regex regex;

        foreach (var pattern in patternCollection)
        {
            regex = new Regex(pattern, RegexOptions.IgnoreCase);
            sb = new StringBuilder(regex.Replace(sb.ToString(), String.Empty));
        }

        regex = new Regex("<[^>]*>", RegexOptions.IgnoreCase);

        sb = new StringBuilder(regex.Replace(sb.ToString(), String.Empty));

        regex = new Regex("{[^}]*}", RegexOptions.IgnoreCase);
        sb = new StringBuilder(regex.Replace(sb.ToString(), String.Empty));

        regex = new Regex("[<]", RegexOptions.IgnoreCase);
        sb = new StringBuilder(regex.Replace(sb.ToString(), "<"));

        regex = new Regex("[>]", RegexOptions.IgnoreCase);
        sb = new StringBuilder(regex.Replace(sb.ToString(), ">"));

        //Special symbols
        var symbolsForReplace = new[] { "&", "/", "\\", "&", "?", "=", "quot;", "nbsp;", "rsquo;", "ndash;", "<", ">" };

        foreach (var word in symbolsForReplace)
        {
            sb.Replace(word, " ");
        }

        sb.Replace("<p>", String.Empty);
        sb.Replace("<p/>", String.Empty);
        sb.Replace("<p />", String.Empty);
        sb.Replace("<P>", String.Empty);
        sb.Replace("<P />", String.Empty);
        sb.Replace("<div />", String.Empty);
        sb.Replace("<div>", String.Empty);
        sb.Replace("<span />", String.Empty);
        sb.Replace("<span>", String.Empty);
        sb.Replace("&quot;", String.Empty);
        sb.Replace("\r", " ");
        sb.Replace("\n", " ");
        sb.Replace("&laquo;", String.Empty);
        sb.Replace("&raquo;", String.Empty);

        return sb.ToString().Trim();
    }

    /// <summary>
    /// Extracts the most frequent keywords from the provided text.
    /// </summary>
    /// <param name="text">Input text from which to extract keywords. HTML content will be converted to plain text preserving line breaks.</param>
    /// <param name="count">Maximum number of keywords to return ordered by frequency (descending).</param>
    /// <returns>
    /// A comma-separated string containing up to <paramref name="count"/> keywords. Only words longer than 4 characters are considered.
    /// Keywords are lowercased, deduplicated and ordered by occurrence frequency.
    /// </returns>
    /// <remarks>
    /// This method uses <see cref="ToPlainText(string, bool)"/> to normalize the input and <see cref="SystemCharacters"/> to remove punctuation.
    /// Words of length 4 or less are ignored. The algorithm is simplistic and intended for basic keyword extraction.
    /// </remarks>
    public static string GetKeywords(string text, int count)
    {
        text = ToPlainText(text, true);
        text = Replace(text, SystemCharacters, String.Empty);
        text = text.Trim().ToLower();

        var keywords = text.Split(' ').ToList();

        keywords = (from k in keywords
            where k.Length > 4
            select k.Trim().ToLower()).ToList();

        var uniqueKeywords = keywords.Distinct().ToList();

        var uniqueKeywordsDicitonary = uniqueKeywords.ToDictionary(uniqueKeyword => uniqueKeyword,
            uniqueKeyword => keywords.Count(keyword => keyword == uniqueKeyword));

        var topKeywords = (from uk in uniqueKeywordsDicitonary
            orderby uk.Value descending
            select uk).Take(count).ToList();

        keywords = topKeywords.Select(x => x.Key).ToList();

        var result = string.Join(", ", keywords);
        
        return result;
    }
    
    /// <summary>
    /// Cut the text to the specified length, attempting to preserve a logical block (sentence) boundary.
    /// </summary>
    /// <param name="text">Input text to truncate. If shorter than or equal to <paramref name="maxLength"/>, it is returned unchanged.</param>
    /// <param name="maxLength">Maximum allowed length. Default is 200 characters.</param>
    /// <returns>
    /// If the text length is less than or equal to <paramref name="maxLength"/>, returns the original text.
    /// Otherwise, tries to find the last period ('.') before <paramref name="maxLength"/> and returns the text up to and including that period.
    /// If no suitable period is found, returns a substring of length <paramref name="maxLength"/> followed by an ellipsis ("...").
    /// </returns>
    public static string CutText(string text, int maxLength = 200)
    {
        if (text.Length <= maxLength)
        {
            return text;
        }

        // Find the first dot before the maxLength
        var dotIndex = text.LastIndexOf('.', maxLength);

        if (dotIndex != -1)
        {
            return text.Substring(0, dotIndex + 1); // Include the dot to keep a logical block
        }

        // If no suitable dot is found, return up to maxLength
        return text.Substring(0, maxLength) + "...";
    }
}