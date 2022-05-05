using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ターン数指定の「期限」
    /// </summary>
    public record TurnDuration(int Value = 0) : Duration(DurationType.FromTurn)
    {
        /// <summary>
        ///     ターン数
        /// </summary>
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