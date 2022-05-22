using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record FailedResult([NotNull] PositionTileAction Action, FailedReason Reason) : PositionTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
