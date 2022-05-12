using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record FailedResult([NotNull] Tile Tile, [NotNull] FailedProcess Process) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
