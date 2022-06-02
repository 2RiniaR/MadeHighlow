using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateCard;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record CreateCardFailedResult(
        [NotNull] PlaceCardAction Action,
        [CanBeNull] Event<ReactedResult<DropCard.SucceedResult>> DropCardEvent,
        [NotNull] CreateCardResult Failed
    ) : PlaceCardResult;
}
