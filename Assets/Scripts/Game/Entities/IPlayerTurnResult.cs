namespace Game.Entities
{
    public interface IPlayerTurnResult

    {
        public IPlayerTurnAction TriedAction { get; }
    }
}