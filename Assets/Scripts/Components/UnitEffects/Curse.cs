using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「呪い」
    /// </summary>
    /// <remarks>
    ///     毎ターン、自身の周囲 N マス以内にいる味方ユニットがダメージを受ける。
    /// </remarks>
    public record Curse(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}