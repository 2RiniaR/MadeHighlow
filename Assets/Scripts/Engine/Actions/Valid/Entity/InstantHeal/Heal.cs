using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record Heal(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public Health Caused([NotNull] Health health)
        {
            return new Health(health.Value + Value);
        }
    }
}
