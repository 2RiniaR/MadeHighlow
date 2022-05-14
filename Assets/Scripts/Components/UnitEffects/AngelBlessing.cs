using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「天使の加護」
    /// </summary>
    /// <remarks>
    ///     自身の周囲 N マス以内にいる味方ユニットが、一部の状態エフェクトにかからなくなる。
    /// </remarks>
    public record AngelBlessing(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}
