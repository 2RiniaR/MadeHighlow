using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    /// <summary>
    ///     ダメージ軽減
    /// </summary>
    public record DamageReduction(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        /// <summary>
        ///     軽減後のダメージを取得する
        /// </summary>
        [NotNull]
        public Damage Caused([NotNull] Damage health)
        {
            return new Damage(health.Value - Value);
        }
    }
}
