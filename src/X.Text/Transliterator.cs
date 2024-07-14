using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace X.Text;

/// <summary>
/// 
/// </summary>
[PublicAPI]
public class Transliterator
{
    private static readonly Dictionary<string, string> Cyrrilic;

    static Transliterator()
    {
        Cyrrilic = new Dictionary<string, string>
        {

            {"щ", "shch"},
            {"Щ", "Shch"},

            {"ё", "yo"},
            {"Ё", "Yo"},

            {"є", "ye"},
                
            {"ж", "zh"},
            {"Ж", "Zh"},

            {"ї", "yi"},
            {"Ї", "Yi"},

            {"э", "e"},
            {"Э", "E"},

            {"ч", "ch"},
            {"Ч", "Ch"},

            {"ш", "sh"},
            {"Ш", "Sh"},

            {"ц", "ts"},
            {"Ц", "Ts"},

            {"ю", "yu"},
            {"Ю", "Yu"},

            {"Я", "Ya"},
            {"я", "ya"},

            {"ъ", "__"},
            {"Ъ", "__"},

            {"х", "kh"},
            {"Х", "Kh"},

            {"ь", "_"},
            {"Ь", "_"},
                
            {"б", "b"},
            {"Б", "B"},

            {"в", "v"},
            {"В", "V"},

            {"г", "g"},
            {"Г", "G"},

            {"ґ", "g"},
            {"Ґ", "G"},

            {"д", "d"},
            {"Д", "D"},

            {"е", "e"},
            {"Е", "E"},
                

            {"з", "z"},
            {"З", "Z"},

            {"и", "i"},
            {"И", "I"},

            {"й", "y"},
            {"Й", "Y"},

            {"к", "k"},
            {"К", "K"},

            {"л", "l"},
            {"Л", "L"},

            {"м", "m"},
            {"М", "M"},

            {"н", "n"},
            {"Н", "N"},

            {"п", "p"},
            {"П", "P"},

            {"р", "r"},
            {"Р", "R"},

            {"с", "s"},
            {"С", "S"},

            {"т", "t"},
            {"Т", "T"},

            {"о", "o"},
            {"О", "O"},

            {"а", "a"},
            {"А", "A"},

            {"ф", "f"},
            {"Ф", "F"},

            {"і", "i"},
            {"І", "I"},

            {"У", "U"},
            {"у", "u"},

            {"ы", "y"},
            {"Ы", "Y"},
        };
    }

    public static string FromTransliterationToCyrillic(string text)
    {
        foreach (var item in Cyrrilic)
        {
            text = text.Replace(item.Value, item.Key);
        }

        return text;
    }

    public static string FromCyrillicToTransliteration(string text)
    {
        foreach (var item in Cyrrilic)
        {
            text = text.Replace(item.Key, item.Value);
        }

        return text;
    }
}