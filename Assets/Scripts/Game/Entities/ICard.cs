using Game.Primitives;

namespace Game.Entities
{
    public interface ICard
    {
        public CardID ID { get; }
        public IPlayer Owner { get; set; }
    }
}