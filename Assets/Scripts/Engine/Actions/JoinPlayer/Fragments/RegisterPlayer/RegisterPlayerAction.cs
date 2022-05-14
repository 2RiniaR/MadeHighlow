using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    public record RegisterPlayerAction([NotNull] Player InitialProps)
    {
        public RegisterPlayerResult Evaluate(IActionContext context)
        {
            return new RegisterPlayerEvaluator(context, InitialProps).Evaluate();
        }
    }
}
