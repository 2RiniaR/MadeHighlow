using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record Action([NotNull] CardID TargetID) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.DropCard(history, this);
        }
    }
}
