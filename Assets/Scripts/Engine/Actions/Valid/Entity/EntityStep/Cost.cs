using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public sealed record Cost(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public static Cost operator +([NotNull] Cost l, [NotNull] Cost r)
        {
            return new Cost(l.Value + r.Value);
        }

        [NotNull]
        public static Cost operator +([NotNull] Cost l, int r)
        {
            return new Cost(l.Value + r);
        }

        [NotNull]
        public static Cost operator -([NotNull] Cost l, int r)
        {
            return new Cost(l.Value - r);
        }

        [NotNull]
        public static Cost operator *([NotNull] Cost l, float r)
        {
            return new Cost(Mathf.FloorToInt(l.Value * r));
        }

        public static bool operator <([NotNull] Cost l, [NotNull] Cost r)
        {
            return l.Value < r.Value;
        }

        public static bool operator >([NotNull] Cost l, [NotNull] Cost r)
        {
            return l.Value > r.Value;
        }

        public static bool operator <=([NotNull] Cost l, [NotNull] Cost r)
        {
            return l.Value <= r.Value;
        }

        public static bool operator >=([NotNull] Cost l, [NotNull] Cost r)
        {
            return l.Value >= r.Value;
        }
    }
}
