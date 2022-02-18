using Game.Entities;
using Game.Primitives;

namespace Game.Cards
{
    public class SampleCard : ICard
    {
        public CardID ID { get; }
        public IPlayer Owner { get; set; }

        public SampleCard(CardID id)
        {
            ID = id;
        }
    }
}