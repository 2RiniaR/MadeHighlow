using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionTile;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
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
