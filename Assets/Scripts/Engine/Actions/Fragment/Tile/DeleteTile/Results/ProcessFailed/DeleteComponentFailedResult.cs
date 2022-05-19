using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteTile
{
    public record DeleteComponentFailedResult(
        [NotNull] DeleteTileAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] DeleteComponentResult Failed
    ) : DeleteTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
