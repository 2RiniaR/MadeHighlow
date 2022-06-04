using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record RejectedResult(
        [NotNull] Action Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CostEffect>> CostEffectInterrupts,
        [NotNull] Cost ExpendedCost,
        [NotNull] [ItemNotNull] ValueList<Interrupt> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : Result;
}
