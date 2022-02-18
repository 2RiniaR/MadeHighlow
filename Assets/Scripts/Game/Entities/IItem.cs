using Game.Primitives;

namespace Game.Entities
{
    public interface IItem : IFieldObject
    {
        public ItemID ID { get; }
    }
}