using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateTileResult : ISimulatable
    {
        [NotNull] public Tile Tile { get; init; } = Tile.Empty;

        public World Simulate(in World world)
        {
            return Tile.Create(world);
        }
    }
}