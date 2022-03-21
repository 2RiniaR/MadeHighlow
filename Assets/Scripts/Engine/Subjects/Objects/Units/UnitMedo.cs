using System;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units
{
    /// <summary>
    ///     ユニットの「メド」
    /// </summary>
    /// <param name="Value">値</param>
    public record UnitMedo(in int Value = 0)
    {
        public const int MinValue = -4;
        public const int MaxValue = 4;

        /// <summary>
        ///     値
        /// </summary>
        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);
    }
}