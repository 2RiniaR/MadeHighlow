using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record DropCardAction([NotNull] CardID TargetID) : Action<DropCardResult>
    {
        protected override DropCardResult EvaluateBody(IHistory history)
        {
            return new DropCardEvaluator(history, TargetID).Evaluate();
        }
    }
}
