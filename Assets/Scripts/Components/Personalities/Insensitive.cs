using RineaR.MadeHighlow.Engine.Subjects.Objects.Units.Personalities;

namespace RineaR.MadeHighlow.Components.Personalities
{
    /// <summary>
    ///     ユニットの性格「無心」
    /// </summary>
    /// <remarks>
    ///     味方ユニットが自身の近くにいない時、「メド」が上昇する。
    ///     味方ユニットが自身の近くにいる時、「メド」が下降する。
    /// </remarks>
    public record Insensitive() : Personality(new PersonalityType(nameof(Insensitive)));
}