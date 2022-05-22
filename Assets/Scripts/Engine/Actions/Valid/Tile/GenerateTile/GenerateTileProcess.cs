using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileProcess([NotNull] Event<CreateTile.SucceedResult> CreateTileEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateTileEvent);
    }
}
