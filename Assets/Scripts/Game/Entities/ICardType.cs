using Game.Primitives;

namespace Game.Entities
{
    public interface ICardType
    {
        public CardTypeID ID { get; }

        public ICard GenerateCard();
    }
}