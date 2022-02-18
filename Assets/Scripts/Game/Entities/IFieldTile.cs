using Game.Primitives;

namespace Game.Entities
{
    public interface IFieldTile
    {
        public FieldPosition Position { get; set; }
        public int Height { get; set; }
        public int MovingCostFrom(FieldDirection direction);
    }
}