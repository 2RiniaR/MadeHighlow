using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」
    /// </summary>
    public record Entity
    {
        public Entity() : this(EntityType.Entity)
        {
        }

        protected Entity(in EntityType type)
        {
            Type = type;
        }

        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID<Entity> ID { get; init; } = ID<Entity>.None;

        /// <summary>
        ///     種類
        /// </summary>
        public EntityType Type { get; }

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
        ///     コンポーネント
        /// </summary>
        [NotNull]
        public ValueObjectList<EntityComponent> Components { get; init; } = ValueObjectList<EntityComponent>.Empty;

        public static Entity Empty => new(EntityType.Entity);
    }
}