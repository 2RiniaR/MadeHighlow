using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record InstantDamageAction
        (ID SourceID, [NotNull] EntityID TargetID, [NotNull] Damage Damage) : Action<InstantDamageResult>
    {
        protected override InstantDamageResult EvaluateBody(IHistory context)
        {
            return new InstantDamageEvaluator(context, SourceID, TargetID, Damage).Evaluate();
        }
    }
}
