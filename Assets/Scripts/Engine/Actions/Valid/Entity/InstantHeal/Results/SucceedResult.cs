using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public sealed record SucceedResult(
        [NotNull] InstantHealAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealCalculation>> CalculationInterrupts,
        [NotNull] Heal Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealRejection>> RejectionInterrupts
    ) : InstantHealResult;
}
