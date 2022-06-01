namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnEvaluator
    {
        public IncrementTurnResult Evaluate(IHistory history)
        {
            return new IncrementTurnResult(history.World.CurrentTurn.Increment());
        }
    }
}
