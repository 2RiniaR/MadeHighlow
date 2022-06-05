using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record Damage(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public Vitality Caused([NotNull] Vitality vitality)
        {
            return vitality with { Health = new Health(vitality.Health.Value - Value) };
        }
    }
}
