using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「ダメージ減少」
    /// </summary>
    /// <remarks>
    ///     自身が受けるダメージを減少させる。
    /// </remarks>
    public record DamageReduction() : Component(new ComponentType(nameof(DamageReduction)));
}