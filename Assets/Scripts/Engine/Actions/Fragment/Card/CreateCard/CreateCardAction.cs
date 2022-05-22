using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record CreateCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps)
    {
        public CreateCardResult Evaluate(IHistory history)
        {
            return new CreateCardEvaluator(history, this).Evaluate();
        }
    }
}
