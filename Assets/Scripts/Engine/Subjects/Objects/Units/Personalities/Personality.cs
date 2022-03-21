using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units.Personalities
{
    /// <summary>
    ///     「ユニット」の性格
    /// </summary>
    /// <param name="Type">種類</param>
    public abstract record Personality([NotNull] in PersonalityType Type)
    {
        /// <summary>
        ///     種類
        /// </summary>
        [NotNull]
        public PersonalityType Type { get; } = Type;
    }
}