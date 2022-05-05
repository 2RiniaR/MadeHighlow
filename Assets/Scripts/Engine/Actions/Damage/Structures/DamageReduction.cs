using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージ軽減
    /// </summary>
    public record DamageReduction(in int Value = 0)
    {
        /// <summary>
        ///     値
        /// </summary>
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     軽減後のダメージを取得する
        /// </summary>
        [NotNull]
        public Damage Caused([NotNull] in Damage health)
        {
            return new Damage(health.Value - Value);
        }
    }
}