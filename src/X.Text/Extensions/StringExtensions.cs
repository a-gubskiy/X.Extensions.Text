using System.Collections.Generic;
using JetBrains.Annotations;

namespace X.Text.Extensions;

[PublicAPI]
public static class StringExtensions
{
    public static string Substring(this string text, int length)
    {
        return TextHelper.Substring(text, length);
    }

    public static string Substring(this string text, int length, string endPart)
    {
        return TextHelper.Substring(text, length, endPart);
    }

    public static string CleanCharacters(this string text)
    {
        return TextHelper.CleanCharacters(text);
    }

    public static string Replace(this string text, IEnumerable<string> forReplace, string whichReplace)
    {
        return TextHelper.Replace(text, forReplace, whichReplace);
    }

    public static string ToPlainText(this string text, bool saveHtmlLineBreaks)
    {
        return TextHelper.ToPlainText(text, saveHtmlLineBreaks);
    }

    public static string ToPlainText(this string text)
    {
        return TextHelper.ToPlainText(text);
    }

    public static string GetKeywords(this string text, int count)
    {
        return TextHelper.GetKeywords(text, count);
    }
}


