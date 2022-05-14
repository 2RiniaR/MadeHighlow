using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetID) : Action<InstantDeathResult>
    {
        protected override InstantDeathResult EvaluateBody(IHistory history)
        {
            return new InstantDeathEvaluator(history, SourceID, TargetID).Evaluate();
        }
    }
}
