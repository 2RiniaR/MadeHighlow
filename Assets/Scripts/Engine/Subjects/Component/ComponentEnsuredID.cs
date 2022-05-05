using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ComponentEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        [CanBeNull]
        public Component GetFrom([NotNull] in World world)
        {
            return Component.GetAllFrom(world).Find(item => item.EnsuredID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}