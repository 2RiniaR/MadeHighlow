using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     識別子の生成器
    /// </summary>
    /// <param name="Next">次の生成番号</param>
    public sealed record IDGenerator(in uint Next = 1)
    {
        public uint Next { get; } = Next != 0 ? Next : throw new ArgumentException();

        public (ID generated, IDGenerator after) Generate()
        {
            return (ID.From(Next), new IDGenerator(Next + 1));
        }

        public IDGenerator Increment(uint number)
        {
            return new IDGenerator(Next + number);
        }
    }
}