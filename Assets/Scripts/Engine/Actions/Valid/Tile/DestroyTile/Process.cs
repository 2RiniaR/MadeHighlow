using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record Process([NotNull] Event<DeleteTile.SucceedResult> DeleteTileEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteTileEvent);
    }
}
