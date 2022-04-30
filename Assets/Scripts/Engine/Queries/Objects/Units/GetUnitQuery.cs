using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Units
{
    public record GetUnitQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Unit Run([NotNull] in World world)
        {
            var foundObject = new GetObjectQuery { Locator = Locator }.Run(world);
            if (foundObject.ObjectType != ObjectType.Unit) throw new NullReferenceException();
            return (Unit)foundObject;
        }
    }
}