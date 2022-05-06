using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上を歩く移動コスト
    /// </summary>
    public sealed record StepCost(in int Value)
    {
        public int Value { get; } = Math.Max(0, Value);
    }
}