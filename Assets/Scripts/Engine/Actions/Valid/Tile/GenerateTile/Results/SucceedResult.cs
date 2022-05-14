using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record SucceedResult(
        [NotNull] Tile InitialStatus,
        [NotNull] RegisterTileResult RegisterTileResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] Fragment.PositionTile.SucceedResult PositionTileResult,
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
