using System;

namespace RineaR.MadeHighlow.Components.Expressions
{
    /// <summary>
    ///     「体力の量」
    /// </summary>
    public record HealthRange
    {
        private HealthRange(HealthRangeReferenceMethod referenceMethod)
        {
            ReferenceMethod = referenceMethod;
        }

        /// <summary>
        ///     参照方法
        /// </summary>
        public HealthRangeReferenceMethod ReferenceMethod { get; }

        /// <summary>
        ///     定数の「体力の量」
        /// </summary>
        /// <param name="Value">値</param>
        public record FromConstant(int Value)
        {
            private const int MinValue = 0;

            /// <summary>
            ///     値
            /// </summary>
            public int Value { get; } = Math.Max(MinValue, Value);
        }
    }
}
