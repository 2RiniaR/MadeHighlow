using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record StepInReaction([NotNull] in EntityID ReactorEntityID, [NotNull] in Result Result);
}