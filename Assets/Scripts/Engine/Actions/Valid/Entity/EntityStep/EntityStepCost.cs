using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public sealed record EntityStepCost(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public static EntityStepCost operator +([NotNull] EntityStepCost l, [NotNull] EntityStepCost r)
        {
            return new EntityStepCost(l.Value + r.Value);
        }

        [NotNull]
        public static EntityStepCost operator +([NotNull] EntityStepCost l, int r)
        {
            return new EntityStepCost(l.Value + r);
        }

        [NotNull]
        public static EntityStepCost operator -([NotNull] EntityStepCost l, int r)
        {
            return new EntityStepCost(l.Value - r);
        }

        [NotNull]
        public static EntityStepCost operator *([NotNull] EntityStepCost l, float r)
        {
            return new EntityStepCost(Mathf.FloorToInt(l.Value * r));
        }

        public static bool operator <([NotNull] EntityStepCost l, [NotNull] EntityStepCost r)
        {
            return l.Value < r.Value;
        }

        public static bool operator >([NotNull] EntityStepCost l, [NotNull] EntityStepCost r)
        {
            return l.Value > r.Value;
        }

        public static bool operator <=([NotNull] EntityStepCost l, [NotNull] EntityStepCost r)
        {
            return l.Value <= r.Value;
        }

        public static bool operator >=([NotNull] EntityStepCost l, [NotNull] EntityStepCost r)
        {
            return l.Value >= r.Value;
        }
    }
}
