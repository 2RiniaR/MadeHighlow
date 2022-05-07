using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record StepInReaction([NotNull] EntityID ReactorEntityID, [NotNull] Result Result);
}
