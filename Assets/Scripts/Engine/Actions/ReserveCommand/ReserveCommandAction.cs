using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record ReserveCommandAction([NotNull] Command Command) : Action<ReserveCommandResult>
    {
        public override ReserveCommandResult Evaluate(IActionContext context)
        {
            return new ReserveCommandEvaluator(context, Command).Evaluate();
        }
    }
}
