using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record SucceedResult(
        [NotNull] MoveEntityAction Action,
        [NotNull] MoveEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<MoveEntityEffect>> Interrupts
    ) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
