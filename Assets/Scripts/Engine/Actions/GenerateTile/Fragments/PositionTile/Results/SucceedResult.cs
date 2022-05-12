using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.PositionTile
{
    public record SucceedResult([NotNull] Tile PositionedTile) : PositionTileResult
    {
        public override World Simulate(World world)
        {
            return PositionedTile.UpdateIn(world);
        }
    }
}
