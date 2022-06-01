using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record EntityStepAction(
        [NotNull] EntityID TargetID,
        [NotNull] Direction2D Direction,
        [NotNull] EntityStepCost Available
    ) : IValidAction;
}
