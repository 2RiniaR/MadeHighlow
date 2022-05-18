using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteCard;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record DeleteCardFailedResult(
        [NotNull] DropCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>> RemoveComponentEvents,
        [NotNull] DeleteCardResult Failed
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
