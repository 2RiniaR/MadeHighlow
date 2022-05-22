using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record SucceedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] EntityStepProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> CostEffectInterrupts,
        [NotNull] EntityStepCost ExpendedCost,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepRejection>> RejectionInterrupts
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
