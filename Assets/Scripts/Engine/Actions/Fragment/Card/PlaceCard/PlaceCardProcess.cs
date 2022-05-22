using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record PlaceCardProcess(
        [CanBeNull] Event<ReactedResult<DropCard.SucceedResult>> DropCardEvent,
        [NotNull] Event<CreateCard.SucceedResult> CreateCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DropCardEvent).Then(CreateCardEvent);
    }
}
