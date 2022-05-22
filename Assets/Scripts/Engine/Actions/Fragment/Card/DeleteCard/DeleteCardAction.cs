using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record DeleteCardAction([NotNull] CardID TargetID)
    {
        public DeleteCardResult Evaluate(IHistory history)
        {
            return new DeleteCardEvaluator(history, this).Evaluate();
        }
    }
}
