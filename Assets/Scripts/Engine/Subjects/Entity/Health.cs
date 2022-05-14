using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティの体力
    /// </summary>
    public sealed record Health(int Value)
    {
        public const int MinValue = 0;
        public const int MaxValue = 999;
        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);
    }
}
