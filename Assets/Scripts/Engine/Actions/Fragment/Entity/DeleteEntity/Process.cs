using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record Process(
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] Event<UnregisterEntity.SucceedResult> UnregisterEntityEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvents).Then(UnregisterEntityEvent);
    }
}
