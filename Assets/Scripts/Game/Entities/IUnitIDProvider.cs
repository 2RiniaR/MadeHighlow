using Game.Primitives;

namespace Game.Entities
{
    public interface IUnitIDProvider
    {
        public UnitID GetNextID();
    }
}