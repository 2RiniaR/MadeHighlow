using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record HealReduction(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public Heal Caused([NotNull] Heal health)
        {
            return new Heal(health.Value - Value);
        }
    }
}
