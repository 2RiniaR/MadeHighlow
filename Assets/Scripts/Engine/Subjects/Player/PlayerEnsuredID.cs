using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record PlayerEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.Get(in World world)
        {
            return Get(world);
        }

        [CanBeNull]
        public Player Get([NotNull] in World world)
        {
            return Player.All(world).Find(player => player.EnsuredID == this);
        }

        [NotNull]
        public World Delete([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}