using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record SucceedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] EntityStepProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> CostInterrupts,
        [NotNull] EntityStepCost ExpendedCost,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepEffect>> Interrupts
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(Process.ClimbResults)
                .Then(Process.ShiftResult)
                .Then(Process.FallResults)
                .Simulate(world);
        }
    }
}
