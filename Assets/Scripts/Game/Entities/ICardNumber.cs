using Game.Primitives;

namespace Game.Entities
{
    public interface ICardNumber
    {
        public CardNumberID ID { get; }

        public ICard GenerateCard();
    }
}