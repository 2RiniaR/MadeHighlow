using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetTileQuery
    {
        [NotNull] public TileLocator Locator { get; init; } = new();

        [NotNull]
        public Tile Run([NotNull] in World world)
        {
            return world.Tiles.Find(tile => tile.ID == Locator.TileID) ?? throw new NullReferenceException();
        }
    }
}