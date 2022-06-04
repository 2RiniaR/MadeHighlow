using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record Action([NotNull] CardID TargetID) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.PayCard(history, this);
        }
    }
}
