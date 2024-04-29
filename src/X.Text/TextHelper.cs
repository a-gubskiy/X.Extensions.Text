using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace X.Text
{
    /// <summary>
    /// Сlass to simplify common tasks working with text
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<String> SystemCharacters { get; set; }

        static TextHelper()
        {
            SystemCharacters = new[]
                {
                    "&", "?", "^", ":", "/", "\\", "@", "$", "(", ")", "+", "[",
                    "]", "{", "}", "%", "~", ">", "<", "=", "*", "“", "\"", "!",
                    "”", "«", "»", ".", ",", "#", "§", "quot;", "--", ";", "\r", "\n", "\t",
                    "..."
                };
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a first character position.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="length">legth of new text</param>
        /// <returns></returns>
        public static string Substring(string text, int length)
        {
            return Substring(text, length, String.Empty);
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a first character position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <param name="endPart">Text for replce cutted text. For example: 'This is long long te...'</param>
        /// <returns></returns>
        public static string Substring(string text, int length, string endPart)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
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
        /// <param name="title"></param>
        /// <returns></returns>
        public static string CleanCharacters(string title)
        {
            const string space = " ";

            var x = Replace(title, SystemCharacters, space);

            var doubleSpace = String.Format("{0}{0}", space);

            while (x.Contains(doubleSpace))
            {
                x = x.Replace(doubleSpace, space).Trim();
            }

            x = x.Trim().ToLower().Replace(space, "-");

            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="forRepalce"></param>
        /// <param name="whichReplace"></param>
        /// <returns></returns>
        public static string Replace(String text, IEnumerable<String> forRepalce, String whichReplace)
        {
            return forRepalce.Aggregate(text, (current, x) => current.Replace(x, whichReplace));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="saveHtmlLineBreaks"></param>
        /// <returns></returns>
        public static string ToPlainText(string text, bool saveHtmlLineBreaks)
        {
            if (!saveHtmlLineBreaks)
            {
                return ToPlainText(text);
            }

            const string lineBreake = "LLineB77Breake";
            const string doubleLineBreake = lineBreake + lineBreake;

            text = text
                .Replace("<br>", lineBreake)
                .Replace("<br/>", lineBreake)
                .Replace("<br />", lineBreake)
                .Replace("<BR>", lineBreake)
                .Replace("<BR/>", lineBreake)
                .Replace("<BR />", lineBreake)
                .Replace("<P>", lineBreake)
                .Replace("<P/>", lineBreake)
                .Replace("<P />", lineBreake)
                .Replace("</P>", lineBreake)
                .Replace("<p>", lineBreake)
                .Replace("<p/>", lineBreake)
                .Replace("<p />", lineBreake);

            while (text.Contains(doubleLineBreake))
            {
                text = text.Replace(doubleLineBreake, lineBreake);
            }

            text = ToPlainText(text);

            text = text.Replace(lineBreake, "<br />");
            return text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToPlainText(string text)
        {
            var patternCollection = new string[]
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

            Regex regEx;

            foreach (var pattern in patternCollection)
            {
                regEx = new Regex(pattern, RegexOptions.IgnoreCase);
                sb = new StringBuilder(regEx.Replace(sb.ToString(), String.Empty));
            }

            regEx = new Regex("<[^>]*>", RegexOptions.IgnoreCase);

            sb = new StringBuilder(regEx.Replace(sb.ToString(), String.Empty));

            regEx = new Regex("{[^}]*}", RegexOptions.IgnoreCase);
            sb = new StringBuilder(regEx.Replace(sb.ToString(), String.Empty));


            regEx = new Regex("[<]", RegexOptions.IgnoreCase);
            sb = new StringBuilder(regEx.Replace(sb.ToString(), "<"));

            regEx = new Regex("[>]", RegexOptions.IgnoreCase);
            sb = new StringBuilder(regEx.Replace(sb.ToString(), ">"));

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
                                                                       uniqueKeyword => keywords.Where(keyword => keyword == uniqueKeyword).Count());

            var topKeywords = (from uk in uniqueKeywordsDicitonary
                               orderby uk.Value descending
                               select uk).Take(count).ToList();

            keywords = topKeywords.Select(x => x.Key).ToList();

            var result = String.Join(", ", keywords);


            return result;
        }

    }
}
