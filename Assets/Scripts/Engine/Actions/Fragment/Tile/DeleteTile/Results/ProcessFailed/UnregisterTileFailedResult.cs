using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.UnregisterTile;

namespace RineaR.MadeHighlow.Actions.DeleteTile
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
