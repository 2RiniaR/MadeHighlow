using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetID) : Action<InstantDeathResult>
    {
        protected override InstantDeathResult EvaluateBody(IActionContext context)
        {
            return new InstantDeathEvaluator(context, SourceID, TargetID).Evaluate();
        }
    }
}
