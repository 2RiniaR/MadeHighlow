using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterPlayer
{
    public record RegisterPlayerAction([NotNull] Player InitialProps)
    {
        public RegisterPlayerResult Evaluate(IHistory context)
        {
            return new RegisterPlayerEvaluator(context, InitialProps).Evaluate();
        }
    }
}
