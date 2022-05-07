using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CardID(ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Card GetFrom([NotNull] World world)
        {
            return Card.GetAllFrom(world).Find(card => card.CardID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }
    }

    public record CardID<TOption>(ID Content) : CardID(Content)
    {
        [CanBeNull]
        public new Card<TOption> GetFrom([NotNull] World world)
        {
            return Card.GetAllFrom(world).Find(card => card.CardID == this) as Card<TOption>;
        }
    }
}
