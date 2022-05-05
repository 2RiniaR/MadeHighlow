using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「エンティティ」の効果
    /// </summary>
    public abstract record Component : IIdentified
    {
        [NotNull] public IAttachableEnsuredID AttachedID { get; init; } = IAttachableEnsuredID.Empty;

        /// <summary>
        ///     有効期間
        /// </summary>
        [NotNull]
        public Duration Duration { get; init; } = Duration.Unlimited;

        public static Component Empty => new EmptyComponent();

        public ComponentEnsuredID EnsuredID => new() { Content = ID };

        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = ID.None;

        [NotNull]
        public World Create([NotNull] in World world)
        {
            var attached = AttachedID.Get(world) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(attached.Components.Add(this));
            return modifiedAttached.Update(world);
        }

        [NotNull]
        public World Update([NotNull] in World world)
        {
            var attached = AttachedID.Get(world) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(
                attached.Components.ReplaceItem(card => card.EnsuredID == EnsuredID, this)
            );
            return modifiedAttached.Update(world);
        }

        public static ValueObjectList<Component> All([NotNull] in World world)
        {
            return world.GetChildren().WhereType<IAttachable>().SelectMany(item => item.Components);
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