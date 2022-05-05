using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record PlayerEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.GetFrom(in World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Player GetFrom([NotNull] in World world)
        {
            return Player.GetAllFrom(world).Find(player => player.EnsuredID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}