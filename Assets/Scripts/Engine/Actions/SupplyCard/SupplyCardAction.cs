using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SupplyCardAction([NotNull] PlayerID TargetID, [NotNull] Card InitialStatus) : Action<SupplyCardResult>
    {
        public override SupplyCardResult Evaluate(IActionContext context)
        {
            return new SupplyCardEvaluator(context, TargetID, InitialStatus).Evaluate();
        }
    }
}
