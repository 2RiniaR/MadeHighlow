using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record TileID : IAttachableID
    {
        public ID Content { get; init; } = ID.None;

        IAttachable IAttachableID.GetFrom(in World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Tile GetFrom([NotNull] in World world)
        {
            return Tile.GetAllFrom(world).Find(tile => tile.EnsuredID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}