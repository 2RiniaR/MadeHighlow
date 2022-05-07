using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果を与えるアクションに対して与える影響
    /// </summary>
    public record InstantHealEffect
    {
        /// <summary>
        ///     治癒効果の軽減効果
        /// </summary>
        [CanBeNull]
        public HealReduction Reduction { get; init; } = null;

        /// <summary>
        ///     `true`の場合、治癒効果が無効化される
        /// </summary>
        public bool Refused { get; init; } = false;
    }
}
