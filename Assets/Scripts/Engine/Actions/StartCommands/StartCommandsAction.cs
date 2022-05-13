namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsAction : Action<StartCommandsResult>
    {
        public override StartCommandsResult Evaluate(IActionContext context)
        {
            return new StartCommandsEvaluator(context).Evaluate();
        }
    }
}
