using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record RejectedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTile.SucceedResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionTile.SucceedResult PositionTileResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateTileEffect>> Interrupts,
        [NotNull] ComponentID RejectedComponentID
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
