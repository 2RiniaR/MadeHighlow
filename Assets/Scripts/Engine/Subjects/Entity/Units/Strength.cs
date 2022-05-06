using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットの攻撃力
    /// </summary>
    public sealed record Strength(in int Value)
    {
        public const int MinValue = 0;
        public const int MaxValue = 999;

        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);
    }
}