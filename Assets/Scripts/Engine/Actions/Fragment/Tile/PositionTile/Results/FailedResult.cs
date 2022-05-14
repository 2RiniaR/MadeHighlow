using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PositionTile
{
    public record FailedResult([NotNull] TileID TileID, FailedReason Reason) : PositionTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
