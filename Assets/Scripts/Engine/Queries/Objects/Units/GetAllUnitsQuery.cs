using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Units;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Units
{
    public record GetAllUnitsQuery
    {
        [NotNull]
        public ImmutableList<Unit> Run([NotNull] in World world)
        {
            return world.Objects.Where(@object => @object.ObjectType == ObjectType.Unit)
                .Select(@object => @object as Unit ?? throw new DataTypeContradictionException())
                .ToImmutableList();
        }
    }
}