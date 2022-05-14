using JetBrains.Annotations;

namespace RineaR.MadeHighlow.ActionFragments.PositionTile
{
    public record FailedResult([NotNull] TileID TileID, FailedReason Reason) : PositionTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
