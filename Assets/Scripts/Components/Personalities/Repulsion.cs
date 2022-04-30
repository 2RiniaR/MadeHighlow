using RineaR.MadeHighlow.Personalities;

namespace RineaR.MadeHighlow.Components.Personalities
{
    /// <summary>
    ///     ユニットの性格「特別」
    /// </summary>
    /// <remarks>
    ///     固有カードを使用したとき、「メド」が上昇する。
    ///     共通カードを使用したとき、「メド」が下降する。
    /// </remarks>
    public record Repulsion() : Personality(new PersonalityType(nameof(Repulsion)));
}