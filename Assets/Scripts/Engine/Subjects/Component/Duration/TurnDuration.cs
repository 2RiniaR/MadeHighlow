using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ターン数の期限
    /// </summary>
    public sealed record TurnDuration(int Value = 0) : Duration
    {
        public int Value { get; } = Math.Max(0, Value);

        public override Duration Decrement()
        {
            if (Value == 0)
            {
                return null;
            }

            return new TurnDuration(Value - 1);
        }
    }
}
