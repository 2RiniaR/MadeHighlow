using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Interact.Effects
{
    /// <summary>
    ///     「ダメージ」
    /// </summary>
    public record Damage : InteractionEffect
    {
        private Damage(DamageReferenceMethod referenceMethod) : base(InteractionEffectType.Damage)
        {
            ReferenceMethod = referenceMethod;
        }

        /// <summary>
        ///     参照方法
        /// </summary>
        public DamageReferenceMethod ReferenceMethod { get; }

        /// <summary>
        ///     定数の「ダメージ」
        /// </summary>
        /// <param name="Value">ダメージ量</param>
        public record FromConstant
        (
            int Value
        ) : Damage(DamageReferenceMethod.FromConstant)
        {
            private const int MinValue = 0;

            /// <summary>
            ///     ダメージ量
            /// </summary>
            public int Value { get; } = Math.Max(MinValue, Value);
        }

        /// <summary>
        ///     ユニットの攻撃力を参照する「ダメージ」
        /// </summary>
        /// <param name="Unit">参照元のユニット</param>
        public record FromUnitStrength
        (
            [NotNull] ObjectLocator Unit
        ) : Damage(DamageReferenceMethod.FromUnitStrength);
    }
}