using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record TileID(ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Tile GetFrom([NotNull] World world)
        {
            return Tile.GetAllFrom(world).Find(tile => tile.TileID == this);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }
    }
}
