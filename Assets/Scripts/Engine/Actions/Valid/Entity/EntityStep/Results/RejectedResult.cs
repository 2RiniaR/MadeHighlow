using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record RejectedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] EntityStepProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> CostEffectInterrupts,
        [NotNull] EntityStepCost ExpendedCost,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
