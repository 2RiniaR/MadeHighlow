using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    public record RegisterPlayerAction([NotNull] Player InitialProps) : Action<RegisterPlayerResult>
    {
        public override RegisterPlayerResult Evaluate(IActionContext context)
        {
            return new RegisterPlayerEvaluator(context, InitialProps).Evaluate();
        }
    }
}
