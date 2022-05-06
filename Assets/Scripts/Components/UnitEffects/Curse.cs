using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「呪い」
    /// </summary>
    /// <remarks>
    ///     毎ターン、自身の周囲 N マス以内にいる味方ユニットがダメージを受ける。
    /// </remarks>
    public record Curse(in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
        in ID,
        in AttachedID,
        in Duration
    );
}