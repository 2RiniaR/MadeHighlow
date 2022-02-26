using Game.Entities;
using Game.Primitives;

namespace Game.Maps
{
    public class SampleMap : IMap
    {
        public MapID ID { get; }

        public IField GenerateField()
        {
            throw new System.NotImplementedException();
        }
    }
}