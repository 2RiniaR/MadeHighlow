using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record InstantHealAction
        (ID SourceID, [NotNull] EntityID TargetID, [NotNull] Heal Heal) : Action<InstantHealResult>
    {
        protected override InstantHealResult EvaluateBody(IActionContext context)
        {
            return new InstantHealEvaluator(context, SourceID, TargetID, Heal).Evaluate();
        }
    }
}
