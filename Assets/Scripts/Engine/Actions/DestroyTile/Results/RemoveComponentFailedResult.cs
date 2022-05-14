using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record RemoveComponentFailedResult(
        [NotNull] Tile Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyTileEffect>> Interrupts,
        [NotNull] ValueList<ReactedResult<RemoveComponent.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<RemoveComponentResult> FailedResult
    ) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
