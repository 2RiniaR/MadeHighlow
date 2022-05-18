using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record RemoveComponentFailedResult(
        [NotNull] DropCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>> RemoveComponentEvents,
        [NotNull] ReactedResult<RemoveComponentResult> Failed
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
