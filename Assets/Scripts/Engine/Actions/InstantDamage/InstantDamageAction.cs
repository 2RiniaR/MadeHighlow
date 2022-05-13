using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record InstantDamageAction
        (ID SourceID, [NotNull] EntityID TargetID, [NotNull] Damage Damage) : Action<InstantDamageResult>
    {
        public override InstantDamageResult Evaluate(IActionContext context)
        {
            return new InstantDamageEvaluator(context, SourceID, TargetID, Damage).Evaluate();
        }
    }
}
