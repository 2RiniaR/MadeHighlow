using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PutCard
{
    public record PutCardAction([NotNull] CardID TargetID)
    {
        public PutCardResult Evaluate(IHistory history)
        {
            return new PutCardEvaluator(history, TargetID).Evaluate();
        }
    }
}
