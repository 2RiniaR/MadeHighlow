using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] Action Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CostEffect>> CostEffectInterrupts,
        [NotNull] Cost ExpendedCost
    );
}
