using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.PutCard
{
    public record PutCardAction([NotNull] CardID TargetID)
    {
        public PutCardResult Evaluate(IActionContext context)
        {
            return new PutCardEvaluator(context, TargetID).Evaluate();
        }
    }
}
