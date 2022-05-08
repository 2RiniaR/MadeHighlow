using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイル
    /// </summary>
    public record Tile(
        ID ID,
        [NotNull] Position2D Position2D,
        [NotNull] Direction2D Direction2D,
        [NotNull] Elevation Elevation,
        [NotNull] [ItemNotNull] ValueList<Component> Components
    ) : IIdentified, IAttachable
    {
        public TileID TileID => new(ID);

        IAttachableID IAttachable.AttachableID => TileID;

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(World world)
        {
            return world with { Tiles = world.Tiles.ReplaceItem(tile => tile.TileID == TileID, this) };
        }

        [NotNull]
        public World CreateIn([NotNull] World world)
        {
            return world with { Tiles = world.Tiles.Add(this) };
        }

        [NotNull]
        public static ValueList<Tile> GetAllFrom([NotNull] World world)
        {
            return world.Tiles;
        }

        [NotNull]
        [ItemNotNull]
        public ValueList<IObject> GetChildren()
        {
            return ValueList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren())
            );
        }
    }
}
