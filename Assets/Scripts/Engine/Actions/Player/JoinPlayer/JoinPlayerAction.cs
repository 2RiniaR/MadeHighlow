using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : Action<JoinPlayerResult>
    {
        protected override JoinPlayerResult EvaluateBody(IHistory history)
        {
            return new JoinPlayerEvaluator(history, InitialPlayer).Evaluate();
        }
    }
}
