namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsAction : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.StartCommands(history, this);
        }
    }
}
