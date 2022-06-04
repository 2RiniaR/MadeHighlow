using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> Calculations,
        [NotNull] Heal Calculated
    );
}
