using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record Process(
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] Event<UnregisterCard.SucceedResult> UnregisterCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvents).Then(UnregisterCardEvent);
    }
}
