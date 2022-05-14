using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record PayCardAction([NotNull] CardID TargetID) : Action<PayCardResult>
    {
        protected override PayCardResult EvaluateBody(IHistory history)
        {
            return new PayCardEvaluator(history, TargetID).Evaluate();
        }
    }
}
