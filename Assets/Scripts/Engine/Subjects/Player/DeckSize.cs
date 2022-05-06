using System;

namespace RineaR.MadeHighlow
{
    public record DeckSize(in int Value)
    {
        public int Value { get; } = Math.Max(1, Value);
    }
}