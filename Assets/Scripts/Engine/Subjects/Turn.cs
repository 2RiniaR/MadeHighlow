using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record Turn(in int Value = 0)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public Turn Increment()
        {
            if (Value == int.MaxValue) return this;
            return new Turn(Value + 1);
        }
    }
}