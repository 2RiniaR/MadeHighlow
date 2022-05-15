using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public record RunCommandAction([NotNull] Command Command) : ValidAction<RunCommandResult>
    {
        protected override RunCommandResult EvaluateBody(IHistory history)
        {
            return new RunCommandEvaluator(history, Command).Evaluate();
        }
    }
}
