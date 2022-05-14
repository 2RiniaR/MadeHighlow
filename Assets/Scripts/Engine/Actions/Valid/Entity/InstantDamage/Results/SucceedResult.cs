using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public sealed record SucceedResult(
        ID SourceID,
        [NotNull] Entity Target,
        [NotNull] Damage Expected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageEffect>> Interrupts,
        [NotNull] Damage Calculated
    ) : InstantDamageResult
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
