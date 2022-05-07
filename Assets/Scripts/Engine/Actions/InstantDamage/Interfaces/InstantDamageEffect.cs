using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えるアクションに対して与える影響
    /// </summary>
    public record InstantDamageEffect(
        [CanBeNull] Ordered<DamageReduction> Reduction,
        [CanBeNull] Ordered<bool> Refused
    );
}
