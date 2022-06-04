using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public sealed record SucceedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> CalculationInterrupts,
        [NotNull] Heal Calculated
    ) : Result;
}
