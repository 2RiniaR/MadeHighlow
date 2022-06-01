using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public record UnregisterTileAction([NotNull] TileID TargetID);
}
