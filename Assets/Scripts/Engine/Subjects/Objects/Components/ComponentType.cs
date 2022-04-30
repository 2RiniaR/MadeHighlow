using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティの効果」の種類
    /// </summary>
    public record ComponentType([NotNull] in string Name);
}