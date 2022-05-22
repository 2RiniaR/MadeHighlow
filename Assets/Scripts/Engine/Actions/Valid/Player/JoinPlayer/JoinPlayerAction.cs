using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : ValidAction<JoinPlayerResult>
    {
        protected override JoinPlayerResult EvaluateBody(IHistory history)
        {
            return new JoinPlayerEvaluator(history, this).Evaluate();
        }
    }
}
