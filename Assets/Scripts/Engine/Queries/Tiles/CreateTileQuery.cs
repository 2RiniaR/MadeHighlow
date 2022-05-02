using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record CreateTileQuery
    {
        [NotNull] public Tile Value { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return world with
            {
                Tiles = world.Tiles.Add(Value),
            };
        }
    }
}