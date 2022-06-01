using JetBrains.Annotations;

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
    }
}
