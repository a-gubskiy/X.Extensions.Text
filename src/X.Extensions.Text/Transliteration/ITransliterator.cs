using JetBrains.Annotations;

namespace X.Extensions.Text.Transliteration;

/// <summary>
/// Allows to transliterate text from one alphabet to another
/// </summary>
[PublicAPI]
public interface ITransliterator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    string FromTransliteration(string text);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    string ToTransliteration(string text);
}