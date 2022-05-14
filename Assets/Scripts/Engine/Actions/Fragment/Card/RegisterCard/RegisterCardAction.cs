using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterCard
{
    public record RegisterCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps)
    {
        public RegisterCardResult Evaluate(IHistory history)
        {
            return new RegisterCardEvaluator(history, ParentID, InitialProps).Evaluate();
        }
    }
}
