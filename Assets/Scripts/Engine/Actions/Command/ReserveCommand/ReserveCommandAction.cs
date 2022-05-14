using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record ReserveCommandAction([NotNull] Command Command) : Action<ReserveCommandResult>
    {
        protected override ReserveCommandResult EvaluateBody(IHistory context)
        {
            return new ReserveCommandEvaluator(context, Command).Evaluate();
        }
    }
}
