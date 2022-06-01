using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SupplyCardAction([NotNull] PlayerID TargetID, [NotNull] Card InitialStatus) : IValidAction;
}
