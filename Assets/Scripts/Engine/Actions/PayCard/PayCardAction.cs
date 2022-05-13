using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record PayCardAction([NotNull] CardID TargetID) : Action<PayCardResult>
    {
        public override PayCardResult Evaluate(IActionContext context)
        {
            return new PayCardEvaluator(context, TargetID).Evaluate();
        }
    }
}
