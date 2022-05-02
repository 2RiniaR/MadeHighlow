using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetEntityComponentQuery
    {
        [NotNull] public EntityComponentLocator Locator { get; init; } = new();

        [NotNull]
        public EntityComponent Run([NotNull] in World world)
        {
            return new GetEntityQuery { Locator = Locator }.Run(world)
                .Components.Find(card => card.ID == Locator.ComponentID) ?? throw new NullReferenceException();
        }
    }
}