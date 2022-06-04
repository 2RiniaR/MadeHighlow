using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record Action([NotNull] TileID TargetID, [NotNull] Position2D Destination);
}
