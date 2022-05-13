using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record RegisterFailedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTileResult FailedResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
