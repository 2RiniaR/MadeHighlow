using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record BigBangAction([NotNull] World InitialWorld) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.BigBang(history, this);
        }
    }
}
