using System;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public sealed record EntityStepCost(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);
    }
}
