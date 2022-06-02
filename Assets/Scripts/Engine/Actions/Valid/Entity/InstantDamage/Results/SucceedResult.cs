using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public sealed record SucceedResult(
        [NotNull] InstantDamageAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageCalculation>> CalculationInterrupts,
        [NotNull] Damage Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageRejection>> RejectionInterrupts
    ) : InstantDamageResult;
}
