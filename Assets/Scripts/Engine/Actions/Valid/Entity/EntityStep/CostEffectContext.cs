using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record CostEffectContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt<CostEffect>> Collected
    );
}
