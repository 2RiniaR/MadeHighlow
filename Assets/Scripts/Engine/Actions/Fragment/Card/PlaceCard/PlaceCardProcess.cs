using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public record PlaceCardProcess(
        [CanBeNull] Event<ReactedResult<Valid.DropCard.SucceedResult>> DropCardEvent,
        [NotNull] Event<CreateCard.SucceedResult> CreateCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DropCardEvent).Then(CreateCardEvent);
    }
}
