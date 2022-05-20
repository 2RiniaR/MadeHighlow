using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record DamageReduction(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public Damage Caused([NotNull] Damage health)
        {
            return new Damage(health.Value - Value);
        }
    }
}
