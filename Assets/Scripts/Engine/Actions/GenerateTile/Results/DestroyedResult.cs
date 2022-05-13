using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record DestroyedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTile.SucceedResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionTile.SucceedResult PositionTileResult
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
