using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SupplyCardAction([NotNull] PlayerID TargetID, [NotNull] Card InitialStatus) : Action<SupplyCardResult>
    {
        protected override SupplyCardResult EvaluateBody(IHistory context)
        {
            return new SupplyCardEvaluator(context, TargetID, InitialStatus).Evaluate();
        }
    }
}
