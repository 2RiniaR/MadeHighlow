using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record PayCardAction([NotNull] CardID TargetID) : Action<PayCardResult>
    {
        protected override PayCardResult EvaluateBody(IActionContext context)
        {
            return new PayCardEvaluator(context, TargetID).Evaluate();
        }
    }
}
