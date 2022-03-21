using System;

namespace RineaR.MadeHighlow.Components.Expressions
{
    /// <summary>
    ///     「ダメージ軽減」
    /// </summary>
    public record DamageReduction
    {
        private DamageReduction(DamageReductionReferenceMethod referenceMethod)
        {
            ReferenceMethod = referenceMethod;
        }

        /// <summary>
        ///     参照方法
        /// </summary>
        public DamageReductionReferenceMethod ReferenceMethod { get; }

        /// <summary>
        ///     定数の「ダメージ軽減」
        /// </summary>
        /// <param name="Value">ダメージ量</param>
        public record FromConstant
        (
            int Value
        ) : DamageReduction(DamageReductionReferenceMethod.FromConstant)
        {
            private const int MinValue = 0;

            /// <summary>
            ///     ダメージ量
            /// </summary>
            public int Value { get; } = Math.Max(MinValue, Value);
        }

        /// <summary>
        ///     ダメージ量に対する割合の「ダメージ軽減」
        /// </summary>
        /// <param name="Value">割合</param>
        public record FromPercent
        (
            float Value
        ) : DamageReduction(DamageReductionReferenceMethod.FromPercent)
        {
            private const float MinValue = 0;
            private const float MaxValue = 1;

            /// <summary>
            ///     割合
            /// </summary>
            public float Value { get; } = Math.Clamp(Value, MinValue, MaxValue);
        }
    }
}