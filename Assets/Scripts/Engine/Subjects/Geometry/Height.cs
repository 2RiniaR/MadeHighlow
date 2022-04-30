using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「フィールド」の高さ
    /// </summary>
    /// <param name="Value">値</param>
    public sealed record Height(in int Value = 0)
    {
        public const int MinValue = 0;
        public const int MaxValue = 2;

        /// <summary>
        ///     値
        /// </summary>
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