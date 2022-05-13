using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record InstantDeathAction(ID SourceID, [NotNull] EntityID TargetID) : Action<InstantDeathResult>
    {
        public override InstantDeathResult Evaluate(IActionContext context)
        {
            return new InstantDeathEvaluator(context, SourceID, TargetID).Evaluate();
        }
    }
}
