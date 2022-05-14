using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record DestroyedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTileResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] ActionFragments.PositionTile.SucceedResult PositionTileResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
