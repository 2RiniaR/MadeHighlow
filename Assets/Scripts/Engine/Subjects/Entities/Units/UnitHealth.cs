using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットの「体力」
    /// </summary>
    /// <param name="Value">値</param>
    public record UnitHealth(in int Value = 0)
    {
        public const int MinValue = 0;
        public const int MaxValue = 999;

        /// <summary>
        ///     値
        /// </summary>
        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);
    }
}