using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteTile
{
    public record Process(
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] Event<UnregisterTile.SucceedResult> UnregisterCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvents).Then(UnregisterCardEvent);
    }
}
