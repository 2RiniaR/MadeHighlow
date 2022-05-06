using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「タイル」を表現する
    /// </summary>
    public record Tile : IIdentified, IAttachable
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = ID.None;

        /// <summary>
        ///     位置
        /// </summary>
        [NotNull]
        public Position2D Position2D { get; init; } = Position2D.Zero;

        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = Direction2D.XPositive;

        /// <summary>
        ///     高さ
        /// </summary>
        [NotNull]
        public TileHeight Height { get; init; } = new GroundTileHeight();

        /// <summary>
        ///     コンポーネント
        /// </summary>
        public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

        /// <summary>
        ///     空のタイル
        /// </summary>
        [NotNull]
        public static Tile Empty => new();

        public TileEnsuredID EnsuredID => new() { Content = ID };

        IAttachableEnsuredID IAttachable.EnsuredID => EnsuredID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(in World world)
        {
            return world with { Tiles = world.Tiles.ReplaceItem(tile => tile.EnsuredID == EnsuredID, this) };
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