using Game.Entities;
using Game.Primitives;

namespace Game.CardNumbers
{
    public class SampleCardNumber : ICardNumber
    {
        public CardNumberID ID { get; }
        public ICard GenerateCard()
        {
            throw new System.NotImplementedException();
        }
    }
}