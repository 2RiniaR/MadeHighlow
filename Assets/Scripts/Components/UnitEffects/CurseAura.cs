using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「カースオーラ」
    /// </summary>
    /// <remarks>
    ///     与えるダメージが N 上昇し、近接攻撃時を行った対象の敵ユニットに「呪い」を付与する。
    /// </remarks>
    public record CurseAura() : Component(new ComponentType(nameof(CurseAura)));
}