using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record StepInReaction([NotNull] EntityID ReactorEntityID, [NotNull] Result Result);
}
