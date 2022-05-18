using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterCard
{
    public record UnregisterCardAction([NotNull] CardID TargetID)
    {
        public UnregisterCardResult Evaluate(IHistory history)
        {
            return new UnregisterCardEvaluator(history, this).Evaluate();
        }
    }
}
