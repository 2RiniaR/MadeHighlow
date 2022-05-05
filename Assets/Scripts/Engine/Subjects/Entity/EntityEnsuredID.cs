using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record EntityEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.GetFrom(in World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Entity GetFrom([NotNull] in World world)
        {
            return Entity.GetAllFrom(world).Find(entity => entity.EnsuredID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}