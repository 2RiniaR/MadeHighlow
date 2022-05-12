using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    /// <summary>
    ///     治癒
    /// </summary>
    public record Heal(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     治癒を与えた後の体力を返す
        /// </summary>
        [NotNull]
        public Health Caused([NotNull] Health health)
        {
            return new Health(health.Value - Value);
        }
    }
}
