using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public record RejectedResult(
        [NotNull] InstantHealAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealCalculation>> CalculationInterrupts,
        [NotNull] Heal Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
