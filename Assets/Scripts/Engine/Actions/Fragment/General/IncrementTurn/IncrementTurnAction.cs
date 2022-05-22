namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public record IncrementTurnAction
    {
        public IncrementTurnResult Evaluate(IHistory history)
        {
            return new IncrementTurnResult(this, history.World.CurrentTurn.Increment());
        }
    }
}
