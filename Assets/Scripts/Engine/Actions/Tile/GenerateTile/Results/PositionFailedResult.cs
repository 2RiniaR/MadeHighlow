using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.PositionTile;
using RineaR.MadeHighlow.ActionFragments.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record PositionFailedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTileResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] PositionTileResult FailedResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
