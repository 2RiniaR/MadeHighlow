using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateTileResult : Result
    {
        [NotNull] public Tile Tile { get; init; } = Tile.Empty;

        public override World Simulate(in World world)
        {
            return Tile.CreateIn(world);
        }
    }
}