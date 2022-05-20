using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public record DestroyTileProcess([NotNull] Event<Fragment.DeleteTile.SucceedResult> DeleteTileEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteTileEvent);
    }
}
