using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : Action<JoinPlayerResult>
    {
        public override JoinPlayerResult Evaluate(IActionContext context)
        {
            return new JoinPlayerEvaluator(context, InitialPlayer).Evaluate();
        }
    }
}
