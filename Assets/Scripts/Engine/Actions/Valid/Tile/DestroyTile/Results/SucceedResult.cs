using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public record SucceedResult(
        [NotNull] DestroyTileAction Action,
        [NotNull] DestroyTileProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyTileRejection>> RejectionInterrupts
    ) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
