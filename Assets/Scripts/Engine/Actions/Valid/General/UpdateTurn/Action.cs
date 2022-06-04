namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public record Action : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.UpdateTurn(history, this);
        }
    }
}
