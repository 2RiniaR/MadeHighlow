using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record AddComponentFailedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTile.SucceedResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> SucceedResults,
        [NotNull] AddComponentResult FailedResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
