using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での高さ
    /// </summary>
    public sealed record Height(in int Value)
    {
        public const int MinValue = 0;
        public const int MaxValue = 2;

        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);

        [NotNull]
        public static Height operator +([NotNull] in Height l, in int r)
        {
            return new Height(l.Value + r);
        }

        [NotNull]
        public static Height operator -([NotNull] in Height l, in int r)
        {
            return new Height(l.Value - r);
        }
    }
}