using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record EntityID(ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Entity GetFrom([NotNull] World world)
        {
            return Entity.GetAllFrom(world).Find(entity => entity.EntityID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }
    }
}