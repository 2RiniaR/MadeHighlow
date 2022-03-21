using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Units;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Units
{
    public record GetUnitQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Unit Run([NotNull] in World world)
        {
            var foundObject = new GetObjectQuery { Locator = Locator }.Run(world);
            if (foundObject.ObjectType != ObjectType.Unit) throw new NotExistException();
            return foundObject as Unit ?? throw new DataTypeContradictionException();
        }
    }
}