using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record PositionTileAction([NotNull] TileID TargetID, [NotNull] Position2D Destination);
}
