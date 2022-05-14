using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer
{
    public record RegisterPlayerAction([NotNull] Player InitialProps)
    {
        public RegisterPlayerResult Evaluate(IHistory history)
        {
            return new RegisterPlayerEvaluator(history, InitialProps).Evaluate();
        }
    }
}
