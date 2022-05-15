using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record StepOutReaction([NotNull] EntityID ReactorEntityID, [NotNull] Result Result);
}
