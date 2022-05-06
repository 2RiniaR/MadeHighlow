using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えるアクションに対して与える影響
    /// </summary>
    public record InstantDamageEffect([CanBeNull] in DamageReduction Reduction, in bool Refused);
}