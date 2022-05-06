using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「カースオーラ」
    /// </summary>
    /// <remarks>
    ///     与えるダメージが N 上昇し、近接攻撃時を行った対象の敵ユニットに「呪い」を付与する。
    /// </remarks>
    public record CurseAura(in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(in ID, in AttachedID, in Duration);
}