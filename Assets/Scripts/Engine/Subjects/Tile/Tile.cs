using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイル
    /// </summary>
    public record Tile(
        in ID ID,
        [NotNull] in Position2D Position2D,
        [NotNull] in Direction2D Direction2D,
        [NotNull] in Elevation Elevation,
        [NotNull] [ItemNotNull] in ValueObjectList<Component> Components
    ) : IIdentified, IAttachable
    {
        public TileID TileID => new(ID);

        IAttachableID IAttachable.AttachableID => TileID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(in World world)
        {
            return world with { Tiles = world.Tiles.ReplaceItem(tile => tile.TileID == TileID, this) };
        }

        [NotNull]
        public World CreateIn([NotNull] in World world)
        {
            return world with { Tiles = world.Tiles.Add(this) };
        }

        [NotNull]
        public static ValueObjectList<Tile> GetAllFrom([NotNull] in World world)
        {
            return world.Tiles;
        }

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IObject> GetChildren()
        {
            return ValueObjectList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren())
            );
        }
    }
}