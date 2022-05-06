using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「天使の加護」
    /// </summary>
    /// <remarks>
    ///     自身の周囲 N マス以内にいる味方ユニットが、一部の状態エフェクトにかからなくなる。
    /// </remarks>
    public record AngelBlessing
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}