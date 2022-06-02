using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record DestroyedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] SupplyCardProcess Process
    ) : SupplyCardResult;
}
