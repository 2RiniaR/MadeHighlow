using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record StepInReaction([NotNull] EntityID ReactorEntityID, [NotNull] Result Result);
}
