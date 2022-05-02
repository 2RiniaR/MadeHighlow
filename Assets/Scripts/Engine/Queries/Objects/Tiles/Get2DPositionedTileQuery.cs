using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Tiles
{
    public class Get2DPositionedTileQuery
    {
        [NotNull] public Position2D Position2D { get; init; } = new();

        [CanBeNull]
        public Tile Run([NotNull] in World world)
        {
            return (Tile)world.Objects.Items.Find(
                @object => @object.Position2D == Position2D && @object.ObjectType == ObjectType.Tile
            );
        }
    }
}