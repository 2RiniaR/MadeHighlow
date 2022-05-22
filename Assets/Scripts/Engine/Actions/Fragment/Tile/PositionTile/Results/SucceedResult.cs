using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record SucceedResult([NotNull] PositionTileAction Action, [NotNull] Tile Positioned) : PositionTileResult
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
