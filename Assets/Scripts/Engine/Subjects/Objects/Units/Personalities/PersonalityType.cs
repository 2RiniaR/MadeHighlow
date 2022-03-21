using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units.Personalities
{
    /// <summary>
    ///     「ユニットの性格」の種類
    /// </summary>
    public record PersonalityType([NotNull] in string Name);
}