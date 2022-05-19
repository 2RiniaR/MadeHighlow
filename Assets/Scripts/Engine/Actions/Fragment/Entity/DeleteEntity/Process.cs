using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteEntity
{
    public record Process(
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] Event<UnregisterEntity.SucceedResult> UnregisterCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvents).Then(UnregisterCardEvent);
    }
}
