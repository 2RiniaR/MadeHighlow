using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record DeleteTileAction([NotNull] TileID TargetID);
}
