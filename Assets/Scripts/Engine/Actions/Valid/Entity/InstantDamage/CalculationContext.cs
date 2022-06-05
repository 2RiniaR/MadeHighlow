using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record CalculationContext(
        [NotNull] IHistory History,
        [NotNull] ReserveCommand.Result Result,
        [NotNull] ValueList<Interrupt<Calculation>> Collected
    );
}
