using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record PlaceCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps)
    {
        public PlaceCardResult Evaluate(IHistory history)
        {
            return new PlaceCardEvaluator(history, this).Evaluate();
        }
    }
}
