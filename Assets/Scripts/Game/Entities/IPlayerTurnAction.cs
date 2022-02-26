using Game.Primitives;

namespace Game.Entities
{
    public interface IPlayerTurnAction

    {
        public int Turn { get; }
        public IPlayer Player { get; }

        public ActionType Type { get; }
        public ICard UsedCard { get; }
        public IUnit ActorUnit { get; }
        public IPlayerTurnResult Result { get; set; }

        public bool IsValid();
    }
}