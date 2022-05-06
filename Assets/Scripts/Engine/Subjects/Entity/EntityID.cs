using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record EntityID(ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(in World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Entity GetFrom([NotNull] in World world)
        {
            return Entity.GetAllFrom(world).Find(entity => entity.EntityID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}