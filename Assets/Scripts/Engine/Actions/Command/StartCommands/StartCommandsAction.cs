namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsAction : Action<StartCommandsResult>
    {
        protected override StartCommandsResult EvaluateBody(IHistory context)
        {
            return new StartCommandsEvaluator(context).Evaluate();
        }
    }
}
