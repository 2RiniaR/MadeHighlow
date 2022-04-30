using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects
{
    public record GetObjectQuery
    {
        [NotNull] public ObjectLocator Locator { get; init; } = new();

        [NotNull]
        public Object Run([NotNull] in World world)
        {
            return world.Objects.Items.Find(@object => @object.ID == Locator.ObjectID) ??
                   throw new NullReferenceException();
        }
    }
}