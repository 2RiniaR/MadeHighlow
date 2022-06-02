using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record CostOverResult(
        [NotNull] EntityStepAction Action,
        [NotNull] EntityStepProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> CostEffectInterrupts,
        [NotNull] EntityStepCost Required
    ) : EntityStepResult;
}
