using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Entities;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Entities
{
    public record GetAllEntitiesQuery
    {
        [NotNull]
        public ImmutableList<Entity> Run([NotNull] in World world)
        {
            return world.Objects.Where(@object => @object.ObjectType == ObjectType.Entity)
                .Select(@object => @object as Entity ?? throw new DataTypeContradictionException())
                .ToImmutableList();
        }
    }
}