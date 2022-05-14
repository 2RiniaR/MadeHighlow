using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PutCard
{
    public record PutCardAction([NotNull] CardID TargetID)
    {
        public PutCardResult Evaluate(IHistory history)
        {
            return new PutCardEvaluator(history, TargetID).Evaluate();
        }
    }
}
