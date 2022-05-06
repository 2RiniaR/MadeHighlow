using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record PlayerID(in ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(in World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Player GetFrom([NotNull] in World world)
        {
            return Player.GetAllFrom(world).Find(player => player.PlayerID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}