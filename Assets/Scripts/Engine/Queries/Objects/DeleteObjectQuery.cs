using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects
{
    public record DeleteObjectQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}