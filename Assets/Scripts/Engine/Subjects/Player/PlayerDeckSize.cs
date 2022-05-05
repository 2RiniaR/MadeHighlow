using System;

namespace RineaR.MadeHighlow
{
    public record PlayerDeckSize(in int Value = 1)
    {
        public int Value { get; } = Math.Max(1, Value);
    }
}