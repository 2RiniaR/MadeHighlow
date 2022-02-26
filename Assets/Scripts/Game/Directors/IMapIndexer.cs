using Game.Entities;
using Game.Primitives;

namespace Game.Directors
{
    public interface IMapIndexer
    {
        public IMap GetByID(MapID id);
    }
}