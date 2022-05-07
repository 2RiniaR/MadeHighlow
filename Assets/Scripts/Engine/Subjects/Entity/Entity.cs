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
        [NotNull] [ItemNotNull] ValueObjectList<Component> Components
    ) : IIdentified, IAttachable
    {
        public EntityID EntityID => new(ID);

        IAttachableID IAttachable.AttachableID => EntityID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
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
        [ItemNotNull]
        public static ValueObjectList<Entity> GetAllFrom([NotNull] World world)
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