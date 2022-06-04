using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record Process(
        [CanBeNull] Event<ReactedResult<DropCard.SucceedResult>> DropCardEvent,
        [NotNull] Event<CreateCard.SucceedResult> CreateCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DropCardEvent).Then(CreateCardEvent);
    }
}
