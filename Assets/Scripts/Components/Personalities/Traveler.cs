using RineaR.MadeHighlow.Engine.Subjects.Objects.Units.Personalities;

namespace RineaR.MadeHighlow.Components.Personalities
{
    /// <summary>
    ///     ユニットの性格「旅人」
    /// </summary>
    /// <remarks>
    ///     長距離を移動したとき、「メド」が上昇する。
    ///     同じ場所に留まっているとき、「メド」が下降する。
    /// </remarks>
    public record Traveler() : Personality(new PersonalityType(nameof(Traveler)));
}