using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateTile;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
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
