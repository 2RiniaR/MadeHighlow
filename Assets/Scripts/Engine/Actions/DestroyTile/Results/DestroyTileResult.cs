using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileResult([NotNull] TileID DestroyedTileID) : Result
    {
        public override World Simulate(World world)
        {
            return DestroyedTileID.DeleteFrom(world);
        }
    }
}
