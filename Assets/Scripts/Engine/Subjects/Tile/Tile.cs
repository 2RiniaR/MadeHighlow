using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
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
    }
}
