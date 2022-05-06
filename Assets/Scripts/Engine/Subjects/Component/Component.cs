using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネント
    /// </summary>
    public abstract record Component(
        in ID ID,
        [NotNull] in IAttachableID AttachedID,
        [NotNull] in Duration Duration
    ) : IIdentified, IComponent
    {
        public ComponentID ComponentID => new(ID);

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
                attached.Components.ReplaceItem(card => card.ComponentID == ComponentID, this)
            );
            return modifiedAttached.UpdateIn(world);
        }

        [NotNull]
        [ItemNotNull]
        public static ValueObjectList<Component> GetAllFrom([NotNull] in World world)
        {
            return world.GetChildren().WhereType<IAttachable>().SelectMany(item => item.Components);
        }

        [NotNull]
        [ItemNotNull]
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
    }
}