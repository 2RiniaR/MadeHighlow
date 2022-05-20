using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record SucceedResult(
        [NotNull] EntityFlyAction Action,
        [NotNull] EntityFlyProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyRejection>> RejectionInterrupts
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
