using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record GetComponentQuery
    {
        [NotNull] public ComponentLocator Locator { get; init; } = new();

        [NotNull]
        public Component Run([NotNull] in World world)
        {
            return new GetObjectQuery { Locator = Locator }.Run(world)
                .Components.Items.Find(card => card.ID == Locator.ComponentID) ?? throw new NullReferenceException();
        }
    }
}