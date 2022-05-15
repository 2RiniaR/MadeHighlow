using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record InstantDamageAction
        (ID SourceID, [NotNull] EntityID TargetID, [NotNull] Damage Damage) : ValidAction<InstantDamageResult>
    {
        protected override InstantDamageResult EvaluateBody(IHistory history)
        {
            return new InstantDamageEvaluator(history, SourceID, TargetID, Damage).Evaluate();
        }
    }
}
