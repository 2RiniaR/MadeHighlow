using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での距離
    /// </summary>
    public sealed record Distance(in int Value)
    {
        public int Value { get; } = Math.Max(0, Value);
    }
}