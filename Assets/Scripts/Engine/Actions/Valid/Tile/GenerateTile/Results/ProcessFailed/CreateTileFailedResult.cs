using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record CreateTileFailedResult(
        [NotNull] GenerateTileAction Action,
        [NotNull] CreateTileResult Failed
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
