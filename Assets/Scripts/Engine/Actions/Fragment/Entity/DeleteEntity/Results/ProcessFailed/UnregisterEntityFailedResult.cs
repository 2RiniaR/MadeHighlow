using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.UnregisterEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteEntity
{
    public record UnregisterEntityFailedResult(
        [NotNull] DeleteEntityAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] UnregisterEntityResult Failed
    ) : DeleteEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
