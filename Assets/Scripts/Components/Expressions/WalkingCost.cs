using System;

namespace RineaR.MadeHighlow.Components.Expressions
{
    /// <summary>
    ///     移動コスト
    /// </summary>
    public record WalkingCost
    (
        int Value
    )
    {
        private const int MinValue = 0;

        /// <summary>
        ///     値
        /// </summary>
        public int Value { get; } = Math.Max(MinValue, Value);
    }
}