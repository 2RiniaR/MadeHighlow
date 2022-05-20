using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record SucceedResult(
        [NotNull] DestroyEntityAction Action,
        [NotNull] DestroyEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityRejection>> RejectionInterrupts
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
