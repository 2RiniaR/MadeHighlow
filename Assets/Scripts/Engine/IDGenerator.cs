using System;

namespace RineaR.MadeHighlow
{
    public record IDGenerator(in uint Next = 1)
    {
        public uint Next { get; } = Next != 0 ? Next : throw new ArgumentException();

        public (ID<T> generated, IDGenerator after) Generate<T>()
        {
            return (ID<T>.From(Next), new IDGenerator(Next + 1));
        }

        public IDGenerator Increment(uint number)
        {
            return new IDGenerator(Next + number);
        }
    }
}