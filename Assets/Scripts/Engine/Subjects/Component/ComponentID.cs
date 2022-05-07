using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ComponentID(ID Content)
    {
        [CanBeNull]
        public Component GetFrom([NotNull] World world)
        {
            return Component.GetAllFrom(world).Find(item => item.ComponentID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }
    }
}