using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.PositionTile
{
    public record PositionTileAction
        ([NotNull] TileID TileID, [NotNull] Position2D Position2D) : Action<PositionTileResult>
    {
        public override PositionTileResult Validate(IActionContext context)
        {
            var tile = TileID.GetFrom(context.World);

            if (tile == null)
            {
                return new FailedResult(TileID, FailedReason.TileNotExist);
            }

            if (!IsPositionable(context, tile, Position2D))
            {
                return new FailedResult(TileID, FailedReason.ResolveFailed);
            }

            return new SucceedResult(tile);
        }

        private static bool IsPositionable(
            [NotNull] IActionContext context,
            [NotNull] Tile tile,
            [NotNull] Position2D dest
        )
        {
            return dest.GetTile(context.World) == null;
        }
    }
}
