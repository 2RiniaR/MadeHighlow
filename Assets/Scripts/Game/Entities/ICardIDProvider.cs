using Game.Primitives;

namespace Game.Entities
{
    public interface ICardIDProvider
    {
        public CardID GetNextID();
    }
}