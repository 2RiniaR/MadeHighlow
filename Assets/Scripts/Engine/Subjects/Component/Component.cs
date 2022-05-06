using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」の効果
    /// </summary>
    public abstract record Component : IIdentified, IComponent
    {
        /// <summary>
        ///     有効期間
        /// </summary>
        [NotNull]
        public Duration Duration { get; init; } = Duration.Unlimited;

        public static Component Empty => new EmptyComponent();
        public IAttachableID AttachedID { get; init; } = IAttachableID.Empty;

        public ComponentID EnsuredID => new() { Content = ID };

        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = ID.None;

        [NotNull]
        public World CreateIn([NotNull] in World world)
        {
            var attached = AttachedID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(attached.Components.Add(this));
            return modifiedAttached.UpdateIn(world);
        }

        [NotNull]
        public World UpdateIn([NotNull] in World world)
        {
            var attached = AttachedID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(
                attached.Components.ReplaceItem(card => card.EnsuredID == EnsuredID, this)
            );
            return modifiedAttached.UpdateIn(world);
        }

        public static ValueObjectList<Component> GetAllFrom([NotNull] in World world)
        {
            return world.GetChildren().WhereType<IAttachable>().SelectMany(item => item.Components);
        }

        public static ValueObjectList<T> GetAllOfTypeFrom<T>([NotNull] in World world) where T : class
        {
            return GetAllFrom(world).WhereType<T>();
        }

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IObject> GetChildren()
        {
            return ValueObjectList<IObject>.Empty;
        }

        private record EmptyComponent : Component;
    }
}