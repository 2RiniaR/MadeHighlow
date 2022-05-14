using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterTile;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.GenerateTile
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
