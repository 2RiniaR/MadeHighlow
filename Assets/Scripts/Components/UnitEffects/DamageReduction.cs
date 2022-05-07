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
        (ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
            ID,
            AttachedID,
            Duration
        );
}