using Game.Primitives;

namespace Game.Entities
{
    public interface IField
    {
        public IFieldTile GetTileAt(FieldPosition position);
    }
}