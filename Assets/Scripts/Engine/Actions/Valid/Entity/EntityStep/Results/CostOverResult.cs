using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record CostOverResult(
        [NotNull] Action Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CostEffect>> CostEffectInterrupts,
        [NotNull] Cost Required
    ) : Result;
}
