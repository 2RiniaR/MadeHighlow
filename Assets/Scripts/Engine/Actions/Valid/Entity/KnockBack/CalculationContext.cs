using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record CalculationContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt<Calculation>> Collected
    );
}
