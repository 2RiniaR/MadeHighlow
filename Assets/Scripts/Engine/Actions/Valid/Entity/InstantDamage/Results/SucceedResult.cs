using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public sealed record SucceedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> CalculationInterrupts,
        [NotNull] Damage Calculated
    ) : Result;
}
