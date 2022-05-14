using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RunCommandAction([NotNull] Command Command) : Action<RunCommandResult>
    {
        protected override RunCommandResult EvaluateBody(IActionContext context)
        {
            return new RunCommandEvaluator(context, Command).Evaluate();
        }
    }
}
