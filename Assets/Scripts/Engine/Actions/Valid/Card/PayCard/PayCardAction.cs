using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record PayCardAction([NotNull] CardID TargetID) : ValidAction<PayCardResult>
    {
        protected override PayCardResult EvaluateBody(IHistory history)
        {
            return new PayCardEvaluator(history, this).Evaluate();
        }
    }
}
