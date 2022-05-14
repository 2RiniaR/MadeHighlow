namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsAction : Action<StartCommandsResult>
    {
        protected override StartCommandsResult EvaluateBody(IHistory history)
        {
            return new StartCommandsEvaluator(history).Evaluate();
        }
    }
}
