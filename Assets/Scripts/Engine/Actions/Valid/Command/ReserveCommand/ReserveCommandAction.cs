using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public record ReserveCommandAction([NotNull] Command Command) : ValidAction<ReserveCommandResult>
    {
        protected override ReserveCommandResult EvaluateBody(IHistory history)
        {
            return new ReserveCommandEvaluator(history, this).Evaluate();
        }
    }
}
