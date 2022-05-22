namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsAction : ValidAction<StartCommandsResult>
    {
        protected override StartCommandsResult EvaluateBody(IHistory history)
        {
            return new StartCommandsEvaluator(history, this).Evaluate();
        }
    }
}
