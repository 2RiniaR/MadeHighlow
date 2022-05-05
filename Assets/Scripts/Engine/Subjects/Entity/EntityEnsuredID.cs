using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record EntityEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.Get(in World world)
        {
            return Get(world);
        }

        [CanBeNull]
        public Entity Get([NotNull] in World world)
        {
            return Entity.All(world).Find(entity => entity.EnsuredID == this);
        }

        [NotNull]
        public World Delete([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}