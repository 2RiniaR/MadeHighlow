using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CardEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.Get(in World world)
        {
            return Get(world);
        }

        [CanBeNull]
        public Card Get([NotNull] in World world)
        {
            return Card.All(world).Find(card => card.EnsuredID == this);
        }

        [NotNull]
        public World Delete([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}