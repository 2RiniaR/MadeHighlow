using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetID) : Action<InstantDeathResult>
    {
        protected override InstantDeathResult EvaluateBody(IHistory context)
        {
            return new InstantDeathEvaluator(context, SourceID, TargetID).Evaluate();
        }
    }
}
