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
            return new Timeline().Then(RegisterTileResult)
                .Then(AddComponentResults)
                .Then(PositionTileResult)
                .Simulate(world);
        }
    }
}
