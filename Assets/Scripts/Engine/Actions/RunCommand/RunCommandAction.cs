using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RunCommandAction([NotNull] Command Command) : Action<RunCommandResult>
    {
        public override RunCommandResult Evaluate(IActionContext context)
        {
            return new RunCommandEvaluator(context, Command).Evaluate();
        }
    }
}
