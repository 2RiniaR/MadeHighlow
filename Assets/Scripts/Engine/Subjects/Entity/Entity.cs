using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」
    /// </summary>
    public record Entity : IIdentified, IAttachable
    {
        /// <summary>
        ///     位置
        /// </summary>
        [NotNull]
        public Position3D Position3D { get; init; } = Position3D.Zero;

        /// <summary>
        ///     向き
        /// </summary>
        [NotNull]
        public Direction3D Direction3D { get; init; } = Direction3D.XNegative;

        /// <summary>
        ///     生命力
        /// </summary>
        [CanBeNull]
        public EntityVitality Vitality { get; init; } = null;

        /// <summary>
        ///     空のエンティティ
        /// </summary>
        [NotNull]
        public static Entity Empty => new();

        public EntityEnsuredID EnsuredID => new() { Content = ID };

        IAttachableEnsuredID IAttachable.EnsuredID => EnsuredID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        /// <summary>
        ///     コンポーネント
        /// </summary>
        public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

        public World Update(in World world)
        {
            return world with { Entities = world.Entities.ReplaceItem(tile => tile.EnsuredID == EnsuredID, this) };
        }

        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = ID.None;

        [NotNull]
        public World Create([NotNull] in World world)
        {
            return world with { Entities = world.Entities.Add(this) };
        }

        [NotNull]
        [ItemNotNull]
        public static ValueObjectList<Entity> All([NotNull] in World world)
        {
            return world.Entities;
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