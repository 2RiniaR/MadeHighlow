using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record CreateCardFailedResult(
        [NotNull] Action Action,
        [CanBeNull] Event<ReactedResult<DropCard.SucceedResult>> DropCardEvent,
        [NotNull] CreateCard.Result Failed
    ) : Result;
}
