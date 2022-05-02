using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetMultiTilesComponentsQuery
    {
        [NotNull]
        public ValueObjectList<TileComponent> Run([NotNull] in World world)
        {
            return world.Tiles.SelectMany(tile => tile.Components);
        }
    }
}