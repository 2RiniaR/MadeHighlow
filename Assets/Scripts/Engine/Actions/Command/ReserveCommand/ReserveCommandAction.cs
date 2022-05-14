using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record ReserveCommandAction([NotNull] Command Command) : Action<ReserveCommandResult>
    {
        protected override ReserveCommandResult EvaluateBody(IHistory history)
        {
            return new ReserveCommandEvaluator(history, Command).Evaluate();
        }
    }
}
