using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record PlayerID(ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Player GetFrom([NotNull] World world)
        {
            return Player.GetAllFrom(world).Find(player => player.PlayerID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }
    }
}
