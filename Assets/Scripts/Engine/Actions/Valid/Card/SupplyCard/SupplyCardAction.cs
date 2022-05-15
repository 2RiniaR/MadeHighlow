using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record SupplyCardAction
        ([NotNull] PlayerID TargetID, [NotNull] Card InitialStatus) : ValidAction<SupplyCardResult>
    {
        protected override SupplyCardResult EvaluateBody(IHistory history)
        {
            return new SupplyCardEvaluator(history, TargetID, InitialStatus).Evaluate();
        }
    }
}
