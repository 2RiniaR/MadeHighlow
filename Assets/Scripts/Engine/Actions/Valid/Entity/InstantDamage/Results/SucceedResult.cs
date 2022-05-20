using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public sealed record SucceedResult(
        [NotNull] InstantDamageAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageCalculation>> CalculationInterrupts,
        [NotNull] Damage Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageRejection>> RejectionInterrupts
    ) : InstantDamageResult
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
