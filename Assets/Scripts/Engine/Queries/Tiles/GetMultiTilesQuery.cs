using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetMultiTilesQuery
    {
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<Tile> Run([NotNull] in World world)
        {
            return world.Tiles;
        }
    }
}