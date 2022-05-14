namespace RineaR.MadeHighlow.Actions.Valid.StartCommands
{
    public record StartCommandsAction : Action<StartCommandsResult>
    {
        protected override StartCommandsResult EvaluateBody(IHistory history)
        {
            return new StartCommandsEvaluator(history).Evaluate();
        }
    }
}
