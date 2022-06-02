namespace RineaR.MadeHighlow.Actions
{
    public abstract record InteractAction : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.Interact(history, this);
        }
    }
}
