using Game.Primitives;

namespace Game.Entities
{
    public interface IFieldObject
    {
        public FieldPosition Position { get; set; }
        public FieldDirection Direction { get; set; }
    }
}