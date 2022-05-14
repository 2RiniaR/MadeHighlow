using System;

namespace RineaR.MadeHighlow.Actions.Valid
{
    /// <summary>
    ///     フィールド上を歩く移動コスト
    /// </summary>
    public sealed record StepCost(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);
    }
}
