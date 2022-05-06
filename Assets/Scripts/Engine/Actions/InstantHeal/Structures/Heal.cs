using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果
    /// </summary>
    public record Heal(in int Value = 0)
    {
        /// <summary>
        ///     値
        /// </summary>
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     治癒効果を与えた後の体力を返す
        /// </summary>
        [NotNull]
        public EntityHealth Caused([NotNull] in EntityHealth health)
        {
            return new EntityHealth(health.Value - Value);
        }
    }
}