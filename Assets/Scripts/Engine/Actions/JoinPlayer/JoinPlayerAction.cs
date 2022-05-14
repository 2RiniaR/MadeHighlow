using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : Action<JoinPlayerResult>
    {
        protected override JoinPlayerResult EvaluateBody(IActionContext context)
        {
            return new JoinPlayerEvaluator(context, InitialPlayer).Evaluate();
        }
    }
}
