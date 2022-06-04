using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record ExemptedResult(
        [NotNull] Action Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt> ExemptionInterrupts,
        [NotNull] ComponentID ExemptedID
    ) : Result;
}
