using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えるアクションに対して与える影響
    /// </summary>
    public record DamageEffect
    {
        /// <summary>
        ///     ダメージ軽減効果
        /// </summary>
        [CanBeNull]
        public DamageReduction Reduction { get; init; } = null;

        /// <summary>
        ///     `true`の場合、ダメージが無効化される
        /// </summary>
        public bool Refused { get; init; } = false;
    }
}