using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Entities
{
    public record GetEntityQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Entity Run([NotNull] in World world)
        {
            var foundObject = new GetObjectQuery { Locator = Locator }.Run(world);
            if (foundObject.ObjectType != ObjectType.Entity) throw new NullReferenceException();
            return (Entity)foundObject;
        }
    }
}