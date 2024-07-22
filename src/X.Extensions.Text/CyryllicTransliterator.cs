using System.Collections.Generic;
using JetBrains.Annotations;

namespace X.Extensions.Text;

/// <summary>
/// Allows to transliterate text from cyrillic to latin and vice versa
/// </summary>
[PublicAPI]
public class CyrillicTransliterator : ITransliterator
{
    private static readonly Dictionary<string, string> Cyrrilic;

    static CyrillicTransliterator()
    {
        Cyrrilic = new Dictionary<string, string>
        {
            { "щ", "shch" },
            { "Щ", "Shch" },

            { "ё", "yo" },
            { "Ё", "Yo" },

            { "ж", "zh" },
            { "Ж", "Zh" },

            { "ї", "yi" },
            { "Ї", "Yi" },

            { "ч", "ch" },
            { "Ч", "Ch" },

            { "ш", "sh" },
            { "Ш", "Sh" },

            { "ц", "ts" },
            { "Ц", "Ts" },

            { "ю", "yu" },
            { "Ю", "Yu" },

            { "Я", "Ya" },
            { "я", "ya" },

            { "ъ", "__" },
            { "Ъ", "__" },

            { "х", "kh" },
            { "Х", "Kh" },

            { "хэ", "he" },
            { "Хэ", "He" },

            { "ь", "_" },
            { "Ь", "_" },

            { "б", "b" },
            { "Б", "B" },

            { "в", "v" },
            { "В", "V" },

            { "во", "wo" },
            { "Во", "Wo" },

            { "г", "g" },
            { "Г", "G" },

            { "ґ", "g" },
            { "Ґ", "G" },

            { "д", "d" },
            { "Д", "D" },

            { "е", "e" },
            { "Е", "E" },

            { "є", "ye" },

            { "з", "z" },
            { "З", "Z" },

            { "и", "i" },
            { "И", "I" },

            { "й", "y" },
            { "Й", "Y" },

            { "к", "k" },
            { "К", "K" },

            { "л", "l" },
            { "Л", "L" },

            { "м", "m" },
            { "М", "M" },

            { "н", "n" },
            { "Н", "N" },

            { "п", "p" },
            { "П", "P" },

            { "р", "r" },
            { "Р", "R" },

            { "с", "s" },
            { "С", "S" },

            { "т", "t" },
            { "Т", "T" },

            { "о", "o" },
            { "О", "O" },

            { "а", "a" },
            { "А", "A" },

            { "ф", "f" },
            { "Ф", "F" },

            { "і", "i" },
            { "І", "I" },

            { "У", "U" },
            { "у", "u" },

            { "ы", "y" },
            { "Ы", "Y" },

            { "э", "e" },
            { "Э", "E" },
        };
    }

    /// <summary>
    /// Converts text from latin transliteration to cyrrilic
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string FromTransliteration(string text)
    {
        foreach (var item in Cyrrilic)
        {
            text = text.Replace(item.Value, item.Key);
        }

        return text;
    }

    /// <summary>
    /// Converts text from cyrrilic to latin transliteration
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string ToTransliteration(string text)
    {
        foreach (var item in Cyrrilic)
        {
            text = text.Replace(item.Key, item.Value);
        }

        return text;
    }
}