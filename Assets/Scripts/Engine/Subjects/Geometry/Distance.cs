using System;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    /// <summary>
    ///     フィールド上の距離
    /// </summary>
    /// <param name="Value">マス数</param>
    public sealed record Distance(in int Value = 0)
    {
        /// <summary>
        ///     マス数
        /// </summary>
        public int Value { get; } = Math.Max(0, Value);
    }
}