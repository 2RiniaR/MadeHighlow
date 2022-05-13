using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record InstantHealAction
        (ID SourceID, [NotNull] EntityID TargetID, [NotNull] Heal Heal) : Action<InstantHealResult>
    {
        public override InstantHealResult Evaluate(IActionContext context)
        {
            return new ActionEvaluator(context, SourceID, TargetID, Heal).Evaluate();
        }
    }
}
