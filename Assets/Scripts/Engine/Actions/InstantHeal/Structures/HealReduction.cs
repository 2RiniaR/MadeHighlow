using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒軽減
    /// </summary>
    public record HealReduction(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     軽減後の治癒を取得する
        /// </summary>
        [NotNull]
        public Heal Caused([NotNull] Heal health)
        {
            return new Heal(health.Value - Value);
        }
    }
}
