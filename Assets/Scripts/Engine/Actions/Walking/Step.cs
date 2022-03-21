using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;

namespace RineaR.MadeHighlow.Engine.Actions.Walking
{
    /// <summary>
    ///     フィールド上の1マスの歩行
    /// </summary>
    public record Step
    {
        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = new();

        /// <summary>
        ///     追加効果
        /// </summary>
        [NotNull]
        public ImmutableList<StepAction> Effects { get; init; } = ImmutableList<StepAction>.Empty;
    }
}