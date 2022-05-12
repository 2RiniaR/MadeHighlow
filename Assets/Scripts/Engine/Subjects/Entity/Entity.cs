using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティ
    /// </summary>
    public record Entity(
        ID ID,
        [NotNull] Position3D Position3D,
        [NotNull] Direction3D Direction3D,
        [CanBeNull] Vitality Vitality,
        bool Levitation,
        [NotNull] [ItemNotNull] ValueList<Component> Components
    ) : IIdentified, IAttachable
    {
        public EntityID EntityID => new(ID);

        IAttachableID IAttachable.AttachableID => EntityID;

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(World world)
        {
            return world with { Entities = world.Entities.ReplaceItem(tile => tile.EntityID == EntityID, this) };
        }

        [NotNull]
        public World CreateIn([NotNull] World world)
        {
            return world with { Entities = world.Entities.Add(this) };
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }

        [NotNull]
        [ItemNotNull]
        public static ValueList<Entity> GetAllFrom([NotNull] World world)
        {
            return world.Entities;
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
