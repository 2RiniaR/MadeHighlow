using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record SucceedResult(
        [NotNull] GenerateTileAction Action,
        [NotNull] GenerateTileProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateTileRejection>> RejectionInterrupts
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
