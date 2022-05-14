using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;
using RineaR.MadeHighlow.Actions.Valid.AddComponent;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record AddComponentFailedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTileResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<AddComponentResult> FailedResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
