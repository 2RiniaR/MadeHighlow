using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Entities;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Entities
{
    public record GetEntityQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Entity Run([NotNull] in World world)
        {
            var foundObject = new GetObjectQuery { Locator = Locator }.Run(world);
            if (foundObject.ObjectType != ObjectType.Entity) throw new NotExistException();
            return foundObject as Entity ?? throw new DataTypeContradictionException();
        }
    }
}