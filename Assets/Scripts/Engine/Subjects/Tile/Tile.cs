using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイル
    /// </summary>
    public record Tile(
        ID ID,
        [NotNull] Position2D Position2D,
        [NotNull] Direction2D Direction2D,
        [NotNull] Elevation Elevation,
        [NotNull] [ItemNotNull] ValueList<Component> Components
    ) : IIdentified, IAttachable
    {
        public TileID TileID => new(ID);

        IAttachableID IAttachable.AttachableID => TileID;

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
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
