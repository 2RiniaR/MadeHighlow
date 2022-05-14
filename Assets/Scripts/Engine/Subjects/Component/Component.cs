using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネント
    /// </summary>
    public abstract record Component(
        ID ID,
        [NotNull] IAttachableID AttachedID,
        [NotNull] Duration Duration
    ) : IIdentified, IComponent
    {
        public ComponentID ComponentID => new(ID);

        [NotNull]
        public World CreateIn([NotNull] World world)
        {
            var attached = AttachedID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(attached.Components.Add(this));
            return modifiedAttached.UpdateIn(world);
        }

        [NotNull]
        public World UpdateIn([NotNull] World world)
        {
            var attached = AttachedID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(
                attached.Components.ReplaceItem(card => card.ComponentID == ComponentID, this)
            );
            return modifiedAttached.UpdateIn(world);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }

        [NotNull]
        [ItemNotNull]
        public static ValueList<Component> GetAllFrom([NotNull] World world)
        {
            return world.GetChildren().WhereType<IAttachable>().SelectMany(item => item.Components);
        }

        [NotNull]
        [ItemNotNull]
        public static ValueList<T> GetAllOfTypeFrom<T>([NotNull] World world) where T : class
        {
            return GetAllFrom(world).WhereType<T>();
        }

        [NotNull]
        [ItemNotNull]
        public ValueList<IObject> GetChildren()
        {
            return ValueList<IObject>.Empty;
        }

        public virtual ValueList<ActionConfirmation> InitializeActions([NotNull] IHistory context)
        {
            return ValueList<ActionConfirmation>.Empty;
        }

        public virtual ValueList<ActionConfirmation> FinalizeActions([NotNull] IHistory context)
        {
            return ValueList<ActionConfirmation>.Empty;
        }
    }
}
