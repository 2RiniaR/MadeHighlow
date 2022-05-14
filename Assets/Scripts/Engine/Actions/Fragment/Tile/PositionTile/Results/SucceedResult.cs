using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PositionTile
{
    public record SucceedResult([NotNull] Tile Positioned) : PositionTileResult
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
