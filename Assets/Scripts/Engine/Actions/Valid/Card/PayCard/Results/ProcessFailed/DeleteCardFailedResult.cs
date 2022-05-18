using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteCard;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record DeleteCardFailedResult(
        [NotNull] PayCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>> RemoveComponentEvents,
        [NotNull] DeleteCardResult Failed
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
