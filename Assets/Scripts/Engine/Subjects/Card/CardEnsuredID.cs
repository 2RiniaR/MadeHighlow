using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CardEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.GetFrom(in World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Card GetFrom([NotNull] in World world)
        {
            return Card.GetAllFrom(world).Find(card => card.EnsuredID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }

    public sealed record CardEnsuredID<TCommand> : CardEnsuredID where TCommand : Command<TCommand>
    {
        [CanBeNull]
        public new Card<TCommand> GetFrom([NotNull] in World world)
        {
            return Card.GetAllFrom(world).Find(card => card.EnsuredID == this) as Card<TCommand>;
        }
    }
}