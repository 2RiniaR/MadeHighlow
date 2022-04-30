using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Tiles
{
    public record GetTileQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Tile Run([NotNull] in World world)
        {
            var foundObject = new GetObjectQuery { Locator = Locator }.Run(world);
            if (foundObject.ObjectType != ObjectType.Tile) throw new NullReferenceException();
            return (Tile)foundObject;
        }
    }
}