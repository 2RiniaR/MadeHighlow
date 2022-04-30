using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「カードの指令」の種類
    /// </summary>
    public record CommandType([NotNull] in string Name);
}