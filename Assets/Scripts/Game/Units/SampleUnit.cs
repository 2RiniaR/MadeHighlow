using Game.Entities;
using Game.Primitives;

namespace Game.Units
{
    public class SampleUnit : IUnit
    {
        public UnitID ID { get; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public FieldPosition Position { get; set; }
        public FieldDirection Direction { get; set; }

        public SampleUnit(UnitID id)
        {
            ID = id;
        }
    }
}