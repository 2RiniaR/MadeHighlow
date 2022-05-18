using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateCard;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public record CreateCardFailedResult(
        [NotNull] PlaceCardAction Action,
        [CanBeNull] Event<ReactedResult<Valid.DropCard.SucceedResult>> DropCardEvent,
        [NotNull] CreateCardResult Failed
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
