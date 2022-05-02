using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetEntityQuery
    {
        [NotNull] public EntityLocator Locator { get; init; } = new();

        [NotNull]
        public Entity Run([NotNull] in World world)
        {
            return world.Entities.Find(entity => entity.ID == Locator.EntityID) ?? throw new NullReferenceException();
        }
    }
}