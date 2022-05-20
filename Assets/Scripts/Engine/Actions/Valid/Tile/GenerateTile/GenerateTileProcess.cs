using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public record GenerateTileProcess([NotNull] Event<Fragment.CreateTile.SucceedResult> CreateTileEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateTileEvent);
    }
}
