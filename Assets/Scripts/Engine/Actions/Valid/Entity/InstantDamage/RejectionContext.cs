using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> Calculations,
        [NotNull] Damage Calculated
    );
}
