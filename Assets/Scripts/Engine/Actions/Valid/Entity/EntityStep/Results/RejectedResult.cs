using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record RejectedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] EntityStepProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> CostInterrupts,
        [NotNull] EntityStepCost ExpendedCost,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
