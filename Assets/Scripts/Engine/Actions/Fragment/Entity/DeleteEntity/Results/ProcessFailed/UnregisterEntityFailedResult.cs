using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.UnregisterEntity;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
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
