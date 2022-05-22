using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteTile;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record DeleteTileFailedResult(
        [NotNull] DestroyTileAction Action,
        [NotNull] DeleteTileResult Failed
    ) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
