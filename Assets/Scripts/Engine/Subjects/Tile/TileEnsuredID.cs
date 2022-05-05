using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record TileEnsuredID : IAttachableEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableEnsuredID.Get(in World world)
        {
            return Get(world);
        }

        [CanBeNull]
        public Tile Get([NotNull] in World world)
        {
            return Tile.All(world).Find(tile => tile.EnsuredID == this);
        }

        [NotNull]
        public World Delete([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}