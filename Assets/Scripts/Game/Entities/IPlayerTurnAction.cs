namespace Game.Entities
{
    public interface IPlayerTurnAction

    {
        public int Turn { get; set; }
        public IPlayer Player { get; set; }
        public ICard UsedCard { get; set; }
        public IUnit ActorUnit { get; set; }
        public IPlayerTurnResult Result { get; set; }
    }
}