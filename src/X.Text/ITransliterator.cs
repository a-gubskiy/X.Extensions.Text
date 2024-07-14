using JetBrains.Annotations;

namespace X.Text;

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