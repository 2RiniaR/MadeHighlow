using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    /// <summary>
    ///     ダメージ
    /// </summary>
    public record Damage(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     ダメージを与えた後の体力を返す
        /// </summary>
        [NotNull]
        public Health Caused([NotNull] Health health)
        {
            return new Health(health.Value - Value);
        }
    }
}
