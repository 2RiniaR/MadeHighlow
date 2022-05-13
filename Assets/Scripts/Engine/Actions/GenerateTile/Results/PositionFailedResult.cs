using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateTile.PositionTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record PositionFailedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTile.SucceedResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionTileResult FailedResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
