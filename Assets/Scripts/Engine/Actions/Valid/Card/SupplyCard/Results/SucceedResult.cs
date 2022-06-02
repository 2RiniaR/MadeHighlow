using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SucceedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] SupplyCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardRejection>> RejectionInterrupts
    ) : SupplyCardResult;
}
