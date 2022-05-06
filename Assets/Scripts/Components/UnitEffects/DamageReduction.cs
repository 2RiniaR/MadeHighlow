using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「ダメージ減少」
    /// </summary>
    /// <remarks>
    ///     自身が受けるダメージを減少させる。
    /// </remarks>
    public record DamageReduction
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}