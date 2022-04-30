namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「カウンター」
    /// </summary>
    /// <remarks>
    ///     自身がいるマスを背後以外から通過したユニットに対して、攻撃を行う。
    /// </remarks>
    public record Counter() : Component(new ComponentType(nameof(Counter)));
}