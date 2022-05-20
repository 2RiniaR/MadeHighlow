using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetID) : ValidAction<InstantDeathResult>
    {
        protected override InstantDeathResult EvaluateBody(IHistory history)
        {
            return new InstantDeathEvaluator(history, this).Evaluate();
        }
    }
}
