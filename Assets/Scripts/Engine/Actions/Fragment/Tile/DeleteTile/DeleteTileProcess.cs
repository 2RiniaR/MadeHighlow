using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record DeleteTileProcess(
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] Event<UnregisterTile.SucceedResult> UnregisterTileEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvents).Then(UnregisterTileEvent);
    }
}
