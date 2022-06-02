using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RunCommandAction([NotNull] Command Command) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.RunCommand(history, this);
        }
    }
}
