using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record SucceedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTile.SucceedResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionTile.SucceedResult PositionTileResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateTileEffect>> Interrupts,
        [NotNull] Tile Generated
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = RegisterTileResult.Simulate(currentWorld);
            currentWorld = AddComponentResults.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            currentWorld = PositionTileResult.Simulate(currentWorld);
            return currentWorld;
        }
    }
}
