using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「死者の加護」
    /// </summary>
    /// <remarks>
    ///     自身の周囲 N マス以内にいる味方ユニットが、ダウンしたときに爆発して呪いをばら撒くようになる。
    /// </remarks>
    public record DeadBlessing
        (ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
            ID,
            AttachedID,
            Duration
        );
}