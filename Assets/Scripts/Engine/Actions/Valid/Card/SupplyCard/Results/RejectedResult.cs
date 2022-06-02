using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record RejectedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] SupplyCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : SupplyCardResult;
}
