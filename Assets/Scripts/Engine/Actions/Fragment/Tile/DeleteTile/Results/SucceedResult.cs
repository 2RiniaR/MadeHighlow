using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteTile
{
    public record SucceedResult
        ([NotNull] DeleteTileAction Action, [NotNull] DeleteTileProcess Process) : DeleteTileResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
