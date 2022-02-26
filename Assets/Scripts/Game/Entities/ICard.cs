using Game.Primitives;

namespace Game.Entities
{
    public interface ICard
    {
        public CardID ID { get; }
        public IPlayer Owner { get; }

        public void Effect(IGameSession session, IUnit unit);
    }
}