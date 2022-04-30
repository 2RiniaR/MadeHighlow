using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record DeleteComponentQuery
    {
        [NotNull] public ComponentLocator Locator { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}