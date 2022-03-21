using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Queries.Objects
{
    public record GetObjectQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Object Run([NotNull] in World world)
        {
            return world.Objects.Find(@object => @object.ID == Locator.ObjectID) ?? throw new NotExistException();
        }
    }
}