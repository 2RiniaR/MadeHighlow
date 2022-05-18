using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record RemoveComponentFailedResult(
        [NotNull] PayCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>> RemoveComponentEvents,
        [NotNull] ReactedResult<RemoveComponentResult> Failed
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
