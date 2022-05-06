using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ComponentID(ID Content)
    {
        [CanBeNull]
        public Component GetFrom([NotNull] in World world)
        {
            return Component.GetAllFrom(world).Find(item => item.ComponentID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}