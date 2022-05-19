using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreatePlayer
{
    public record CreatePlayerAction([NotNull] Player InitialProps)
    {
        public CreatePlayerResult Evaluate(IHistory history)
        {
            return new CreatePlayerEvaluator(history, this).Evaluate();
        }
    }
}
