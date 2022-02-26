using Game.Entities;
using Game.Primitives;

namespace Game.Fields
{
    public class SampleFieldTile : IFieldTile
    {
        public FieldPosition Position { get; set; }
        public int Height { get; set; }
        public int MovingCostFrom(FieldDirection direction)
        {
            throw new System.NotImplementedException();
        }
    }
}