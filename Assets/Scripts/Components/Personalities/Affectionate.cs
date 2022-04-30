using RineaR.MadeHighlow.Personalities;

namespace RineaR.MadeHighlow.Components.Personalities
{
    /// <summary>
    ///     ユニットの性格「愛情」
    /// </summary>
    /// <remarks>
    ///     味方ユニットに補助効果を与えたとき、「メド」が上昇する。
    ///     味方ユニットから補助効果を受けたとき、「メド」が下降する。
    /// </remarks>
    public record Affectionate() : Personality(new PersonalityType(nameof(Affectionate)));
}