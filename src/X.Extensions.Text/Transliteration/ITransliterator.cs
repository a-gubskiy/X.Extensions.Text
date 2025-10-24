using JetBrains.Annotations;

namespace X.Extensions.Text.Transliteration;

/// <summary>
/// Provides methods to transliterate text between alphabets or scripts (for example, Cyrillic ↔ Latin).
/// Implementations define the specific transliteration rules and directionality.
/// </summary>
[PublicAPI]
public interface ITransliterator
{
    /// <summary>
    /// Converts a transliterated string back into the original alphabet or script.
    /// </summary>
    /// <param name="text">Input text in transliterated form (for example, Latin characters representing Cyrillic).</param>
    /// <returns>
    /// The text converted back to the original alphabet/script.
    /// Implementations should return the input unchanged if no conversion is possible.
    /// </returns>
    string FromTransliteration(string text);

    /// <summary>
    /// Converts text from the original alphabet or script into a transliterated representation.
    /// </summary>
    /// <param name="text">Input text in the original alphabet/script (for example, Cyrillic).</param>
    /// <returns>
    /// The transliterated representation (for example, Latin characters).
    /// Implementations should return the input unchanged if transliteration is not required.
    /// </returns>
    string ToTransliteration(string text);
}