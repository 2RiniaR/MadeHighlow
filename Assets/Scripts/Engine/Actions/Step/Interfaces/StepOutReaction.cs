using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record StepOutReaction([NotNull] EntityID ReactorEntityID, [NotNull] Result Result);
}
