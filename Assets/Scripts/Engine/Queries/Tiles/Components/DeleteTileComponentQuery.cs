using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record DeleteTileComponentQuery
    {
        [NotNull] public TileComponentLocator Locator { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}