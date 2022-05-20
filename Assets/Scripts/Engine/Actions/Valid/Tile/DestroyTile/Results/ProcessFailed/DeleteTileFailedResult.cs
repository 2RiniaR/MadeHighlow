using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteTile;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
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
