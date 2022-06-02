using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record SucceedResult([NotNull] PositionTileAction Action, [NotNull] Tile Positioned) : PositionTileResult;
}
