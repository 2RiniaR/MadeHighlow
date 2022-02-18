using Game.Primitives;

namespace Game.Entities
{
    public interface IMap
    {
        public MapID ID { get; }

        public IField GenerateField();
    }
}
