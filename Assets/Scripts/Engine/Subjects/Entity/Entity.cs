using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティ
    /// </summary>
    public record Entity(
        in ID ID,
        [NotNull] in Position3D Position3D,
        [NotNull] in Direction3D Direction3D,
        [CanBeNull] in Vitality Vitality,
        [NotNull] [ItemNotNull] in ValueObjectList<Component> Components
    ) : IIdentified, IAttachable
    {
        public EntityID EntityID => new(ID);

        IAttachableID IAttachable.AttachableID => EntityID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(in World world)
        {
            return world with { Entities = world.Entities.ReplaceItem(tile => tile.EntityID == EntityID, this) };
        }

        [NotNull]
        public World CreateIn([NotNull] in World world)
        {
            return world with { Entities = world.Entities.Add(this) };
        }

        [NotNull]
        [ItemNotNull]
        public static ValueObjectList<Entity> GetAllFrom([NotNull] in World world)
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