using Game.Primitives;

namespace Game.Entities
{
    public interface IUnit : IFieldObject, IAttackable
    {
        public UnitID ID { get; }
        public int Health { get; set; }
        public int Strength { get; set; }
    }
}