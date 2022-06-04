using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CostEffect>> CostEffects,
        [NotNull] Cost Expended
    );
}
