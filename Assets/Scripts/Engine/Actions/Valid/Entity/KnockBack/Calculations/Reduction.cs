using System;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record Reduction(int Value) : KnockBackCalculation
    {
        public int Value { get; } = Math.Max(0, Value);
    }
}
