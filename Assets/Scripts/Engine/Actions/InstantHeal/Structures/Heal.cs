using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果
    /// </summary>
    public record Heal(in int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     治癒効果を与えた後の体力を返す
        /// </summary>
        [NotNull]
        public Health Caused([NotNull] in Health health)
        {
            return new Health(health.Value - Value);
        }
    }
}