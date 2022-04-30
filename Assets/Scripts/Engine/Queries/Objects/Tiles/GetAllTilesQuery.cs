using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Tiles
{
    public record GetAllTilesQuery
    {
        [NotNull]
        public ValueObjectList<Tile> Run([NotNull] in World world)
        {
            return world.Objects.Items.Where(@object => @object.ObjectType == ObjectType.Tile)
                .Cast<Tile>()
                .ToValueObjectList();
        }
    }
}