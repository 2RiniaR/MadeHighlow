using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public record InstantHealAction
        (ID SourceID, [NotNull] EntityID TargetID, [NotNull] Heal Heal) : ValidAction<InstantHealResult>
    {
        protected override InstantHealResult EvaluateBody(IHistory history)
        {
            return new InstantHealEvaluator(history, this).Evaluate();
        }
    }
}
