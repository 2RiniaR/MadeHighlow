using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public sealed record SucceedResult(
        ID SourceID,
        [NotNull] Entity Target,
        [NotNull] Heal Expected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealEffect>> Interrupts,
        [NotNull] Heal Calculated
    ) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            Contract.Requires<InvalidOperationException>(Target.Vitality != null);

            var vitality = Target.Vitality;
            var modifiedTarget = Target with
            {
                Vitality = vitality with { Health = Calculated.Caused(vitality.Health) },
            };
            return modifiedTarget.UpdateIn(world);
        }
    }
}
