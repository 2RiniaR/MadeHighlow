using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetTileByPositionQuery
    {
        [NotNull] public Position2D Position2D { get; init; } = new();

        [CanBeNull]
        public Tile Run([NotNull] in World world)
        {
            return world.Tiles.Find(tile => tile.Position2D == Position2D);
        }
    }
}