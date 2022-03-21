using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Tiles
{
    public record GetAllTilesQuery
    {
        [NotNull]
        public ImmutableList<Tile> Run([NotNull] in World world)
        {
            return world.Objects.Where(@object => @object.ObjectType == ObjectType.Tile)
                .Select(@object => @object as Tile ?? throw new DataTypeContradictionException())
                .ToImmutableList();
        }
    }
}