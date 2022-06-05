using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record CostEffect
    {
        public int? AdditionValue { get; private init; }
        public int? ReductionValue { get; private init; }
        public Cost OverwriteValue { get; private init; }

        public static CostEffect Addition(int value)
        {
            return new CostEffect { AdditionValue = value };
        }

        public static CostEffect Reduction(int value)
        {
            return new CostEffect { ReductionValue = value };
        }

        public static CostEffect Overwrite([NotNull] Cost value)
        {
            return new CostEffect { OverwriteValue = value };
        }
    }
}
