using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record StepOutReaction([NotNull] in EntityID ReactorEntityID, [NotNull] in Result Result);
}