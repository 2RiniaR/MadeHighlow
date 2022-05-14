using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PutCard
{
    public record PutCardAction([NotNull] CardID TargetID)
    {
        public PutCardResult Evaluate(IActionContext context)
        {
            return new PutCardEvaluator(context, TargetID).Evaluate();
        }
    }
}
