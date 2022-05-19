using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;
using RineaR.MadeHighlow.Actions.Fragment.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateEntity
{
    public record CreateComponentFailedResult(
        [NotNull] CreateEntityAction Action,
        [NotNull] Event<RegisterEntityResult> RegisterEntityEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponentResult Failed
    ) : CreateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
