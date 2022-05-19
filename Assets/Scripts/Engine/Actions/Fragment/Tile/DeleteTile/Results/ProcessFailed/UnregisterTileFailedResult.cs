using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.UnregisterTile;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteTile
{
    public record UnregisterTileFailedResult(
        [NotNull] DeleteTileAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] UnregisterTileResult Failed
    ) : DeleteTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
