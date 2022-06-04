using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record Process([NotNull] Event<CreateTile.SucceedResult> CreateTileEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreateTileEvent);
    }
}
