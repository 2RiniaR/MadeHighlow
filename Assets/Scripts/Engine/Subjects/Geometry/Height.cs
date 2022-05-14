using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での高さ
    /// </summary>
    public sealed record Height(int Value) : IComparable<Height>
    {
        public const int MinValue = 0;
        public const int MaxValue = 2;

        public int Value { get; } = Math.Clamp(Value, MinValue, MaxValue);

        [NotNull]
        public static Height operator +([NotNull] Height l, int r)
        {
            return new Height(l.Value + r);
        }

        [NotNull]
        public static Height operator -([NotNull] Height l, int r)
        {
            return new Height(l.Value - r);
        }

        public static bool operator <([NotNull] Height l, [NotNull] Height r)
        {
            return l.Value < r.Value;
        }

        public static bool operator >([NotNull] Height l, [NotNull] Height r)
        {
            return l.Value > r.Value;
        }

        public static bool operator <=([NotNull] Height l, [NotNull] Height r)
        {
            return l.Value <= r.Value;
        }

        public static bool operator >=([NotNull] Height l, [NotNull] Height r)
        {
            return l.Value >= r.Value;
        }

        public int CompareTo(Height other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }
    }
}
