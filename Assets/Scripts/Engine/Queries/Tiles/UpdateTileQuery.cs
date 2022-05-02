using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record UpdateTileQuery
    {
        [NotNull] public TileLocator Locator { get; init; } = new();
        [NotNull] public Tile Value { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return world with
            {
                Tiles = world.Tiles.ReplaceItem(tile => tile.ID == Locator.TileID, Value),
            };
        }
    }
}