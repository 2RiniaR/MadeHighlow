using Game.Primitives;

namespace Game.Entities
{
    public interface IPlayerIDProvider
    {
        public PlayerID GetNextID();
    }
}