using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace X.Extensions.Text;

/// <summary>
/// Class contains methods to simplify common tasks working with text
/// </summary>
[PublicAPI]
public static class TextHelper
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly IReadOnlyCollection<string> SystemCharacters = new[]
    {
        "&", "?", "^", ":", "/", "\\", "@", "$", "(", ")", "+", "[",
        "]", "{", "}", "%", "~", ">", "<", "=", "*", "“", "\"", "!",
        "”", "«", "»", ".", ",", "#", "§", "quot;", "--", ";", "\r", "\n", "\t",
        "..."
    };

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a first character position.
    /// </summary>
    /// <param name="text">Text</param>
    /// <param name="length">Length of new text</param>
    /// <returns></returns>
    public static string Substring(string text, int length)
    {
        return Substring(text, length, string.Empty);
    }

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a first character position.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="length"></param>
    /// <param name="endPart">Text for replace removed text. For example: 'This is long-long te...'</param>
    /// <returns></returns>
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
    /// Clean system characters
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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
    /// <param name="text">The input string in which replacements will be made.</param>
    /// <param name="targets">An enumerable collection of strings to be replaced.</param>
    /// <param name="replacement">The string that will replace each target string.</param>
    /// <returns>The modified string with all specified targets replaced by the replacement string.</returns>
    public static string Replace(string text, IEnumerable<string> targets, string replacement)
    {
        return targets.Aggregate(text, (current, x) => current.Replace(x, replacement));
    }

    /// <summary>
    /// Convert HTML to plain text.
    /// </summary>
    /// <param name="text">The HTML content to be converted.</param>
    /// <param name="preserveLineBreaks">Flag to indicate if HTML line breaks should be preserved.</param>
    /// <returns>The plain text version of the input HTML.</returns>
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
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="lineBreakPlaceholder"></param>
    /// <returns></returns>
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
    /// Try to convert HTML to plain text
    /// </summary>
    /// <param name="text">Input string</param>
    /// <returns></returns>
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
    /// Get keywords from text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="count"></param>
    /// <returns></returns>
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
    /// Cut the text to the specified length, preserving the logical block
    /// </summary>
    /// <param name="text"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
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