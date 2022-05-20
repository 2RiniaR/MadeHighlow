using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record RejectedResult(
        [NotNull] InstantDamageAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageCalculation>> CalculationInterrupts,
        [NotNull] Damage Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
