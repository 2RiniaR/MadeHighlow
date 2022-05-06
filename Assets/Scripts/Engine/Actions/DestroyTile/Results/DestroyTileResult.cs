using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileResult([NotNull] in TileID DestroyedTileID) : Result
    {
        public override World Simulate(in World world)
        {
            return DestroyedTileID.DeleteFrom(world);
        }
    }
}