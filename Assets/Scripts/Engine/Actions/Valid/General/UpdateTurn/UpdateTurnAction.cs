namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public record UpdateTurnAction : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.UpdateTurn(history, this);
        }
    }
}
