using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetUnitQuery
    {
        [NotNull] public EntityLocator Locator { get; init; } = new();

        [NotNull]
        public Unit Run([NotNull] in World world)
        {
            var entity = new GetEntityQuery { Locator = Locator }.Run(world);
            if (entity.Type != EntityType.Unit) throw new NullReferenceException();
            return (Unit)entity;
        }
    }
}