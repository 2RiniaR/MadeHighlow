using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     メド（一般名：進化の進捗）
    /// </summary>
    public sealed record Medo(int Value)
    {
        public const int MinValue = -4;
        public const int MaxValue = 4;
        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);
    }
}
