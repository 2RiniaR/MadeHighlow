using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public sealed record SucceedResult(
        [NotNull] InstantHealAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealCalculation>> CalculationInterrupts,
        [NotNull] Heal Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealRejection>> RejectionInterrupts
    ) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new ArgumentException();
            var vitality = target.Vitality ?? throw new ArgumentException();

            var modifiedTarget = target with
            {
                Vitality = vitality with { Health = Calculated.Caused(vitality.Health) },
            };
            return modifiedTarget.UpdateIn(world);
        }
    }
}
