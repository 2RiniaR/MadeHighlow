using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果の軽減
    /// </summary>
    public record HealReduction(in int Value = 0)
    {
        /// <summary>
        ///     値
        /// </summary>
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     軽減後の治癒効果を取得する
        /// </summary>
        [NotNull]
        public Heal Caused([NotNull] in Heal health)
        {
            return new Heal(health.Value - Value);
        }
    }
}